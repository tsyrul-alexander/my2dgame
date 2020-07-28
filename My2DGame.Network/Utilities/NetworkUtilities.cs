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
		public const int LengthByteCount = 4;
		public const int LengthStartIndex = 0;//0
		public const int LengthByteEndIndex = 3;//3
		public const int QueryTypeStartIndex = 0;//4
		public const int QueryTypeEndIndex = QueryTypeStartIndex;//4
		public const int RoomIdStartIndex = QueryTypeEndIndex + 1;//5
		public const int RoomIdEndIndex = RoomIdStartIndex + 15;//20
		public const int UniqueIdStartIndex = RoomIdEndIndex + 1;//21
		public const int UniqueIdEndIndex = UniqueIdStartIndex + 15;//36
		
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
			IEnumerable<byte> dataArr = ConvertLengthToBytes(data.Length + UniqueIdEndIndex + 1);
			dataArr = dataArr.Concat(new []{ (byte)queryType });
			dataArr = dataArr.Concat(ConvertIdToBytes(roomId));
			dataArr = dataArr.Concat(ConvertIdToBytes(itemId));
			return dataArr.Concat(data);
		}

		public static void GetRequestInfo(this byte[] requestData, out byte[] data, out Guid itemId, out Guid roomId, out QueryType queryType) {
			queryType = (QueryType)requestData[0];
			roomId = ConvertBytesToId(requestData.Range(RoomIdStartIndex, RoomIdEndIndex).ToArray());
			itemId = ConvertBytesToId(requestData.Range(UniqueIdStartIndex, UniqueIdEndIndex));
			data = requestData.Skip(UniqueIdEndIndex + 1).ToArray();
		}

		public static T[] Range<T>(this T[] source, int start, int end) {
			if (end < 0) {
				end = source.Length + end;
			}
			var len = (end - start) + 1;
			var res = new T[len];
			for (var i = 0; i < len; i++) {
				res[i] = source[i + start];
			}
			return res;
		}

		public static byte[] GetMessageBytes(this NetworkStream stream) {
			var len = ConvertByteToLength((byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte(), (byte)stream.ReadByte());
			var data = new byte[len];
			using (var ms = new MemoryStream()) {
				if (stream.DataAvailable) {
					var bytes = stream.Read(data, 0, data.Length);
					ms.Write(data, 0, bytes);
				}
				//do {
				//	var bytes = stream.Read(data, 0, data.Length);
				//	ms.Write(data, 0, bytes);
				//} while (stream.DataAvailable);
				return ms.ToArray();
			}
		}

		private static byte[] ConvertIdToBytes(Guid id) {
			return id.ToByteArray();
		}

		private static byte[] ConvertLengthToBytes(int len) {
			return Int32Converter.ToArray(len);
		}
		private static int ConvertByteToLength(byte value1, byte value2, byte value3, byte value4) {
			return new Int32Converter(value1, value2, value3, value4);
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
		public static object ToObject(this byte[] data,
			Func<BinaryFormatter, SurrogateSelector> selector = null) {
			BinaryFormatter formatter = new BinaryFormatter();
			if (selector != null) {
				formatter.SurrogateSelector = selector(formatter);
			}
			formatter.AssemblyFormat = FormatterAssemblyStyle.Full;
			using (MemoryStream ms = new MemoryStream(data)) {
				object obj = formatter.Deserialize(ms);
				return obj;
			}
		}
	}
}
