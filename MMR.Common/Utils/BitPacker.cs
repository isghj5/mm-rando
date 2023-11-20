using System;
using System.Collections.Generic;

namespace MMR.Common.Utils
{
    public class BitPacker
    {
        private int _bitPosition = 0;
        private int BytePosition => _bitPosition / 8;
        private int BitOfByte => _bitPosition % 8;

        private List<byte> data = new List<byte>();

        public byte[] ToByteArray(int align = 1)
        {
            var result = data.ToArray();
            if (align > 1)
            {
                var padding = align - result.Length % align;
                Array.Resize(ref result, result.Length + padding);
            }
            return result;
        }

        public void Write(bool value)
        {
            var bitshift = 7 - BitOfByte;
            var valueToWrite = (byte)((value ? 1 : 0) << bitshift);
            WriteByte(valueToWrite);
            _bitPosition++;
        }

        public void Write(int value, int bitsToUse)
        {
            var mask = (int)Math.Pow(2, bitsToUse) - 1;
            value &= mask;
            var bitshift = 7 - BitOfByte - bitsToUse + 1;
            byte valueToWrite;
            while (bitshift < 0)
            {
                var rightShift = -bitshift;
                valueToWrite = (byte)(value >> rightShift);
                WriteByte(valueToWrite);
                var bitsUsed = bitsToUse - rightShift;
                _bitPosition += bitsUsed;
                bitshift += 8;
                bitsToUse -= bitsUsed;
            }
            valueToWrite = (byte)(value << bitshift);
            WriteByte(valueToWrite);
            _bitPosition += bitsToUse;
        }

        private void WriteByte(byte value)
        {
            if (BytePosition >= data.Count)
            {
                data.Add(0);
            }
            data[BytePosition] |= value;
        }
    }

    public class BitUnpacker
    {
        private byte[] _data;
        private int _bitPosition = 0;
        private int BytePosition => _bitPosition / 8;
        private int BitOfByte => _bitPosition % 8;
        public BitUnpacker(byte[] data)
        {
            _data = data;
        }

        public bool ReadBool()
        {
            var bitshift = 7 - BitOfByte;
            var value = _data[BytePosition] >> bitshift;
            _bitPosition++;
            return (value & 1) == 1;
        }

        public int ReadS32(int bitsToUse)
        {
            var value = 0;
            var bitshift = 7 - BitOfByte - bitsToUse + 1;
            byte byteValue;
            int mask;
            while (bitshift < 0)
            {
                var leftShift = -bitshift;
                var bitsUsed = bitsToUse - leftShift;
                mask = (int)Math.Pow(2, bitsUsed) - 1;
                byteValue = (byte)(_data[BytePosition] & mask);
                value |= (byteValue << leftShift);

                _bitPosition += bitsUsed;
                bitshift += 8;
                bitsToUse -= bitsUsed;
            }

            mask = (int)Math.Pow(2, bitsToUse) - 1;
            byteValue = (byte)((_data[BytePosition] >> bitshift) & mask);
            value |= byteValue;
            _bitPosition += bitsToUse;

            return value;
        }
    }
}
