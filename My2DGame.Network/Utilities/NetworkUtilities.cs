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
		public const int UniqueIdByteArraySize = 16;
		public const int LengthByteArraySize = 1;
		public const int LengthDivisor = 8;
		public static byte[] ToBytes(this object obj, Func<BinaryFormatter, SurrogateSelector> selector = null) {
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

		public static IEnumerable<byte> GetRequestData(this byte[] data, Guid id) {
			var lenArray = new[] {
				ConvertLengthToByte(data.Length)
			};
			return lenArray.Concat(ConvertIdToBytes(id).Concat(data));
		}

		public static (Guid id, IEnumerable<byte> data) GetRequestIdData(this byte[] data) {
			return (ConvertBytesToId(data.Take(UniqueIdByteArraySize).ToArray()), data.Skip(UniqueIdByteArraySize));
		}

		public static byte[] GetMessageBytes(this NetworkStream stream) {
			var lenByte = (byte)stream.ReadByte();
			var len = ConvertByteToLength(lenByte) + UniqueIdByteArraySize;
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
