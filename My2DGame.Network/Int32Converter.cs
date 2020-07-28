using System;
using System.Runtime.InteropServices;

namespace My2DGame.Network {
	[StructLayout(LayoutKind.Explicit)]
	public struct Int32Converter {
		[FieldOffset(0)]
		public int Value;
		[FieldOffset(0)]
		public byte Byte1;
		[FieldOffset(1)]
		public byte Byte2;
		[FieldOffset(2)]
		public byte Byte3;
		[FieldOffset(3)]
		public byte Byte4;
		public Int32Converter(byte byte1, byte byte2, byte byte3, byte byte4) {
			Value = 0;
			Byte1 = byte1;
			Byte2 = byte2;
			Byte3 = byte3;
			Byte4 = byte4;
		}
		public Int32Converter(int value) {
			Byte1 = Byte2 = Byte3 = Byte4 = 0;
			Value = value;
		}
		public static implicit operator Int32(Int32Converter value) {
			return value.Value;
		}
		public static implicit operator Int32Converter(int value) {
			return new Int32Converter(value);
		}
		public static byte[] ToArray(Int32Converter converter) {
			return new [] {
				converter.Byte1,
				converter.Byte2,
				converter.Byte3,
				converter.Byte4
			};
		}
	}
}