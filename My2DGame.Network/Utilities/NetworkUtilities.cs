using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace My2DGame.Network.Utilities {
	public static class NetworkUtilities {
		public const int QueryTypeStartIndex = 0;
		public const int QueryTypeEndIndex = 1;
		public const int RoomIdStartIndex = 1;
		public const int RoomIdEndIndex = 17;
		public const int UniqueIdStartIndex = 17;
		public const int UniqueIdEndIndex = 33;
		public const int LengthByteArraySize = 1;
		public const int LengthDivisor = 8;
		public static byte[] ToBytes(this object obj, Func<BinaryFormatter, SurrogateSelector> selector = null) {
			if (obj == null) {
				return new byte[0];
			}
			var bf = new BinaryFormatter();
			if (selector != null) {
				bf.SurrogateSelector = selector(bf);
			}
			bf.AssemblyFormat = FormatterAssemblyStyle.Full;
			byte[] data;
			using (var ms = new MemoryStream()) {
				bf.Serialize(ms, obj);
				data = ms.ToArray();
			}
			return data;
		}

		public static IEnumerable<byte> GetRequestData(this byte[] data, Guid itemId, Guid roomId, QueryType queryType) {
			var lenArray = new[] {
				ConvertLengthToByte(data.Length),
				(byte)queryType
			};
			var dataArr = lenArray.Concat(ConvertIdToBytes(roomId));
			dataArr = dataArr.Concat(ConvertIdToBytes(itemId));
			return dataArr.Concat(data);
		}

		public static void GetRequestInfo(this byte[] requestData, out byte[] data, out Guid itemId, out Guid roomId, out QueryType queryType) {
			queryType = (QueryType)requestData[0];
			roomId = ConvertBytesToId(requestData.Range(RoomIdStartIndex, RoomIdEndIndex).ToArray());
			itemId = ConvertBytesToId(requestData.Range(UniqueIdStartIndex, UniqueIdEndIndex));
			data = requestData.Skip(UniqueIdEndIndex).ToArray();
		}

		public static T[] Range<T>(this T[] source, int start, int end) {
			if (end < 0) {
				end = source.Length + end;
			}
			var len = end - start;
			var res = new T[len];
			for (var i = 0; i < len; i++) {
				res[i] = source[i + start];
			}
			return res;
		}

		public static byte[] GetMessageBytes(this NetworkStream stream) {
			var lenByte = (byte)stream.ReadByte();
			var len = ConvertByteToLength(lenByte) + UniqueIdEndIndex;
			var data = new byte[len];
			using (var ms = new MemoryStream()) {
				do {
					var bytes = stream.Read(data, 0, data.Length);
					ms.Write(data, 0, bytes);
				} while (stream.DataAvailable);
				return ms.ToArray();
			}
		}

		private static byte[] ConvertIdToBytes(Guid id) {
			return id.ToByteArray();
		}

		private static byte ConvertLengthToByte(int len) {
			var value = Math.Ceiling(len / (double)LengthDivisor);
			if (value > byte.MaxValue) {
				throw new ArgumentOutOfRangeException(nameof(len));
			}
			return (byte)value;
		}
		private static int ConvertByteToLength(byte value) {
			return value * LengthDivisor;
		}

		public static Guid ConvertBytesToId(byte[] bytes) {
			return new Guid(bytes);
		}
		public static object GetMessageObj(this NetworkStream stream,
			Func<BinaryFormatter, SurrogateSelector> selector = null) {
			var formatter = new BinaryFormatter();
			if (selector != null) {
				formatter.SurrogateSelector = selector(formatter);
			}
			formatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			var obj = formatter.Deserialize(stream);
			return obj;
		}
	}
}
