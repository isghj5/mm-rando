namespace MMR.Common.Extensions
{
    public static class PrimitiveExtensions
    {
        public static byte LeftByte(this ushort @this) => (byte) (@this >> 8);

        public static byte RightByte(this ushort @this) => (byte) (@this & 0xFF);
    }
}
