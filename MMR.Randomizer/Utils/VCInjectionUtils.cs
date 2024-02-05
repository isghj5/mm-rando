using Be.IO;
using MMR.Common.Extensions;
using MMR.Randomizer.Asm;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MMR.Randomizer.Utils
{
    /// <summary>
    /// Controller button flags for Virtual Console.
    /// </summary>
    public enum VCControllerButton : ushort
    {
        DPadUp = 0x800,
        DPadDown = 0x400,
        DPadLeft = 0x200,
        DPadRight = 0x100,
        L = 0x20,
    }

    public class VCInjectionUtils
    {
        /// <summary>
        /// Offset into decompressed App1 file which contains the D-Pad mapping.
        /// </summary>
        static readonly int DPAD_MAPPING_OFFSET = 0x148512;

        private static void GetApp5(byte[] ROM, string VCDir)
        {
            BinaryReader a51 = new BinaryReader(File.OpenRead(Path.Combine(VCDir, "5-1")));
            BinaryWriter app5 = new BinaryWriter(File.Open(Path.Combine(VCDir, "00000005.app"), FileMode.Create));
            byte[] buffer = VCReadA50(ROM.Length - 4, VCDir);
            app5.Write(buffer);
            app5.Write(ROM);
            buffer = new byte[a51.BaseStream.Length];
            a51.Read(buffer, 0, buffer.Length);
            app5.Write(buffer);
            a51.Close();
            app5.Close();
        }

        private static byte[] VCReadA50(int romsize, string VCDir)
        {
            BinaryReader a50 = new BinaryReader(File.OpenRead(Path.Combine(VCDir, "5-0")));
            byte[] buffer = new byte[a50.BaseStream.Length];
            a50.Read(buffer, 0, buffer.Length);

            // If over 32MB, rom size and file offsets in the included "5-0" u8 archive chunk need to be updated
            if (romsize > 0x02000000)
            {
                buffer[0x574] = (byte)(romsize >> 24 & 0xFF);
                buffer[0x575] = (byte)(romsize >> 16 & 0xFF);
                buffer[0x576] = (byte)(romsize >> 8 & 0xFF);
                buffer[0x577] = (byte)((romsize & 0xFF) + 4);

                int entryoffset = 0x0578;
                for (int i = 0; i < 0x4C; i++)
                {
                    if ((buffer[entryoffset] & 0x01) == 0x00)
                    {
                        int fileoffset = ((buffer[entryoffset + 4] << 24) + (buffer[entryoffset + 5] << 16) + (buffer[entryoffset + 6] << 8) + buffer[entryoffset + 7]
                                           + (romsize - 0x02000000));

                        buffer[entryoffset + 4] = (byte)(fileoffset >> 24 & 0xFF);
                        buffer[entryoffset + 5] = (byte)(fileoffset >> 16 & 0xFF);
                        buffer[entryoffset + 6] = (byte)(fileoffset >> 8 & 0xFF);
                        buffer[entryoffset + 7] = (byte)(fileoffset & 0xFF);
                    }
                    entryoffset += 0xC;
                }
            }

            a50.Close();
            return buffer.ToArray();
        }

        private static void DeleteApp5(string VCDir)
        {
            File.Delete(Path.Combine(VCDir, "00000005.app"));
        }

        private static byte[] AddVCHeader(byte[] ROM)
        {
            // This header dictates how much space in wii memory to allocate for the ROM
            byte[] Header;
            if (ROM.Length > 0x02000000)
            {
                int headersize = ROM.Length << 2;
                Header = new byte[] { (byte)(headersize >> 24 & 0xFF), (byte)(headersize >> 16 & 0xFF), (byte)(headersize >> 8 & 0xFF), (byte)(headersize & 0xFF) };
            } else
            {
                Header = new byte[] { 0x08, 0x00, 0x00, 0x00 };
            }
            
            return Header.Concat(ROM).ToArray();
        }

        public static void BuildVC(byte[] ROM, DPadConfig dpadConfig, string VCDir, string FileName)
        {
            ROM = AddVCHeader(ROM);
            GetApp5(ROM, VCDir);
            PatchApp1(dpadConfig, VCDir);
            ProcessStartInfo p = new ProcessStartInfo
            {
                FileName = "wadpacker",
                Arguments = "mm.tik mm.tmd mm.cert \"" + FileName + "\" -i NMRE",
                WorkingDirectory = VCDir,
                UseShellExecute = true,
            };
            var proc = Process.Start(p);

            proc.WaitForExit();

            DeleteApp5(VCDir);
        }

        /// <summary>
        /// Patch app file to map D-Pad directions. This assumes the app file is decompressed.
        /// </summary>
        /// <param name="config">D-Pad configuration</param>
        /// <param name="VCDir">VC directory</param>
        private static void PatchApp1(DPadConfig config, string VCDir)
        {
            using (var app1 = new BeBinaryWriter(File.OpenWrite(Path.Combine(VCDir, "00000001.app"))))
            {
                var used = config.InUse();
                var buttons = new VCControllerButton[]
                {
                    VCControllerButton.DPadUp,
                    VCControllerButton.DPadDown,
                    VCControllerButton.DPadLeft,
                    VCControllerButton.DPadRight,
                };

                // Fix array for ordering of the fields in app file: Up, Down, Left, Right
                used = new bool[] { used[0], used[2], used[3], used[1] };

                for (int i = 0; i < used.Length; i++)
                {
                    int offset = DPAD_MAPPING_OFFSET + (i * 4);
                    app1.Seek(offset, SeekOrigin.Begin);

                    if (used[i])
                    {
                        // If using this D-Pad direction, write its button flag
                        app1.WriteUInt32((uint)buttons[i]);
                    }
                    else
                    {
                        // Otherwise write the button flag for the L button
                        app1.WriteUInt32((uint)VCControllerButton.L);
                    }
                }
            }
        }
    }
}
