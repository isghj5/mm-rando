using MMR.Randomizer.Models.Rom;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace MMR.Randomizer.Utils
{

    public static class ReadWriteUtils
    {

        public static void WriteFileAddr(int[] Addr, byte[] data, byte[] file)
        {
            for (int i = 0; i < Addr.Length; i++)
            {
                Arr_Insert(data, 0, data.Length, file, Addr[i]);
            }
        }

        public static void WriteROMAddr(int[] Addr, byte[] data)
        {
            for (int i = 0; i < Addr.Length; i++)
            {
                int var = (int)(Addr[i] & 0xF0000000) >> 28;
                int rAddr = Addr[i] & 0xFFFFFFF;
                byte[] rdata = data;
                if (var == 1)
                {
                    rdata[0] += 0xA;
                    rdata[1] -= 0x70;
                }
                int f = RomUtils.GetFileIndexForWriting(rAddr);
                int dest = rAddr - RomData.MMFileList[f].Addr;
                Arr_Insert(rdata, 0, rdata.Length, RomData.MMFileList[f].Data, dest);
            }
        }

        public static void WriteToROM(int Addr, byte val)
        {
            int f = RomUtils.GetFileIndexForWriting(Addr);
            int dest = Addr - RomData.MMFileList[f].Addr;
            RomData.MMFileList[f].Data[dest] = val;
        }

        public static void WriteToROM(int Addr, ushort val)
        {
            int f = RomUtils.GetFileIndexForWriting(Addr);
            int dest = Addr - RomData.MMFileList[f].Addr;
            var data = new byte[]
            {
                (byte)((val & 0xFF00) >> 8),
                (byte)(val & 0xFF)
            };
            Arr_Insert(data, 0, data.Length, RomData.MMFileList[f].Data, dest);
        }

        public static void WriteToROM(int Addr, uint val)
        {
            int f = RomUtils.GetFileIndexForWriting(Addr);
            int dest = Addr - RomData.MMFileList[f].Addr;
            var data = new byte[]
            {
                (byte)((val & 0xFF000000) >> 24),
                (byte)((val & 0xFF0000) >> 16),
                (byte)((val & 0xFF00) >> 8),
                (byte)(val & 0xFF)
            };
            Arr_Insert(data, 0, data.Length, RomData.MMFileList[f].Data, dest);
        }

        public static void WriteToROM(int Addr, byte[] val)
        {
            int f = RomUtils.GetFileIndexForWriting(Addr);
            int dest = Addr - RomData.MMFileList[f].Addr;
            Arr_Insert(val, 0, val.Length, RomData.MMFileList[f].Data, dest);
        }

        public static void WriteToROM(int Addr, ReadOnlyMemory<byte> val)
        {
            int f = RomUtils.GetFileIndexForWriting(Addr);
            int dest = Addr - RomData.MMFileList[f].Addr;
            var memory = new Memory<byte>(RomData.MMFileList[f].Data);
            val.CopyTo(memory.Slice(dest));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Arr_Insert(byte[] src, int start, int len, byte[] dest, int addr)
        {
            for (int i = 0; i < len; i++)
            {
                dest[addr + i] = src[start + i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Byteswap32(uint val)
        {
            return ((val & 0xFF) << 24) | ((val & 0xFF00) << 8) | ((val & 0xFF0000) >> 8) | ((val & 0xFF000000) >> 24);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Byteswap16(ushort val)
        {
            return (ushort)(((val & 0xFF) << 8) | ((val & 0xFF00) >> 8));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Arr_ReadU32(byte[] Arr, int Src)
        {
            return (uint)((Arr[Src] << 24) | (Arr[Src + 1] << 16) | (Arr[Src + 2] << 8) | Arr[Src + 3]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Arr_ReadS32(byte[] Arr, int Src)
        {
            return (Arr[Src] << 24) | (Arr[Src + 1] << 16) | (Arr[Src + 2] << 8) | Arr[Src + 3];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Arr_ReadU16(byte[] Arr, int Src)
        {
            return (ushort)((Arr[Src] << 8) | (Arr[Src + 1]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Arr_ReadS16(byte[] Arr, int Src)
        {
            return (short)((Arr[Src] << 8) | (Arr[Src + 1]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Arr_WriteU32(byte[] Arr, int Dest, uint val)
        {
            Arr[Dest] = (byte)((val & 0xFF000000) >> 24);
            Arr[Dest + 1] = (byte)((val & 0xFF0000) >> 16);
            Arr[Dest + 2] = (byte)((val & 0xFF00) >> 8);
            Arr[Dest + 3] = (byte)(val & 0xFF);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Arr_WriteU16(byte[] Arr, int Dest, ushort val)
        {
            Arr[Dest] = (byte)((val & 0xFF00) >> 8);
            Arr[Dest + 1] = (byte)(val & 0xFF);
        }

        public static uint ReadU32(BinaryReader ROM)
        {
            return Byteswap32(ROM.ReadUInt32());
        }

        public static int ReadS32(BinaryReader ROM)
        {
            return (int)ReadU32(ROM);
        }

        public static ushort ReadU16(BinaryReader ROM)
        {
            return Byteswap16(ROM.ReadUInt16());
        }

        public static void WriteU32(BinaryWriter ROM, uint val)
        {
            ROM.Write(Byteswap32(val));
        }

        public static void WriteU16(BinaryWriter ROM, ushort val)
        {
            ROM.Write(Byteswap16(val));
        }

        public static ushort ReadU16(int address)
        {
            int f = RomUtils.GetFileIndexForWriting(address);
            int src = address - RomData.MMFileList[f].Addr;
            return (ushort)((RomData.MMFileList[f].Data[src] << 8)
                + RomData.MMFileList[f].Data[src + 1]);
        }

        public static uint ReadU32(int address)
        {
            int f = RomUtils.GetFileIndexForWriting(address);
            int src = address - RomData.MMFileList[f].Addr;
            return (uint)((RomData.MMFileList[f].Data[src] << 24)
                | (RomData.MMFileList[f].Data[src + 1] << 16)
                | (RomData.MMFileList[f].Data[src + 2] << 8)
                | (RomData.MMFileList[f].Data[src + 3]));
        }

        public static byte Read(int address)
        {
            int f = RomUtils.GetFileIndexForWriting(address);
            int src = address - RomData.MMFileList[f].Addr;
            return RomData.MMFileList[f].Data[src];
        }

        public static byte[] ReadBytes(int address, uint count)
        {
            var f = RomUtils.GetFileIndexForWriting(address);
            var src = address - RomData.MMFileList[f].Addr;
            var bytes = new byte[count];
            Array.Copy(RomData.MMFileList[f].Data, src, bytes, 0, count);
            return bytes;
        }

        /// <summary>
        /// Copy bytes from a source array to a dest array of a specific length.
        /// </summary>
        /// <param name="src">Source array</param>
        /// <param name="length">Dest length</param>
        /// <returns>Dest bytes</returns>
        public static byte[] CopyBytes(byte[] src, uint length)
        {
            var dest = new byte[length];
            var amount = Math.Min(src.Length, dest.Length);
            for (var i = 0; i < amount; i++)
                dest[i] = src[i];
            return dest;
        }

        /// <summary>
        /// Write 16-bit <see cref="ushort"/> value to ROM as big-endian.
        /// </summary>
        /// <param name="addr">VROM address</param>
        /// <param name="value">Value</param>
        public static void WriteU16ToROM(int addr, ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            WriteToROM(addr, bytes);
        }

        /// <summary>
        /// Write 32-bit <see cref="uint"/> value to ROM as big-endian.
        /// </summary>
        /// <param name="addr">VROM address</param>
        /// <param name="value">Value</param>
        public static void WriteU32ToROM(int addr, uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            WriteToROM(addr, bytes);
        }

        /// <summary>
        /// Write 64-bit <see cref="ulong"/> value to ROM as big-endian.
        /// </summary>
        /// <param name="addr">VROM address</param>
        /// <param name="value">Value</param>
        public static void WriteU64ToROM(int addr, ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            WriteToROM(addr, bytes);
        }

        /// <summary>
        /// VRAM load address of <c>code</c> file.
        /// </summary>
        const uint CodeRAMStart = 0x800A5AC0;

        /// <summary>
        /// Get the <see cref="MMFile"/> at a <see cref="FileIndex"/>.
        /// </summary>
        /// <param name="index">File index.</param>
        /// <returns></returns>
        public static MMFile GetFile(FileIndex index)
        {
            RomUtils.CheckCompressed((int)index);
            return RomData.MMFileList[(int)index];
        }

        /// <summary>
        /// Write a <c>NOP</c> instruction to the <c>code</c> file.
        /// </summary>
        /// <param name="vram">VRAM address within loaded <c>code</c> file.</param>
        public static void WriteCodeNOP(uint vram)
        {
            WriteCodeUInt32(vram, 0);
        }

        /// <summary>
        /// Write a 32-bit <see cref="uint"/> value to the <c>code</c> file.
        /// </summary>
        /// <param name="vram">VRAM address within loaded <c>code</c> file.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteCodeUInt32(uint vram, uint value)
        {
            var offset = vram - CodeRAMStart;
            var file = GetFile(FileIndex.code);
            var span = new Span<byte>(file.Data);
            var slice = span.Slice((int)offset, 4);
            WriteUInt32(slice, value);
        }

        /// <summary>
        /// Write a 32-bit <see cref="uint"/> value to a <see cref="Span{byte}"/> in big-endian order.
        /// </summary>
        /// <param name="span">Span to write to.</param>
        /// <param name="value">Value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt32(Span<byte> span, uint value)
        {
            span[0] = (byte)((value >> 24) & 0xFF);
            span[1] = (byte)((value >> 16) & 0xFF);
            span[2] = (byte)((value >> 8) & 0xFF);
            span[3] = (byte)(value & 0xFF);
        }
    }

}
