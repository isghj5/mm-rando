using System.Security.Cryptography;

namespace MMR.Randomizer.Constants
{
    /// <summary>
    /// Contains hashes for player model object data.
    /// </summary>
    public static class PlayerModelHash
    {
        /// <summary>
        /// <see cref="MD5"/> hash of Link MM model.
        /// </summary>
        public static byte[] LinkMM = new byte[] {
            0x93, 0xFD, 0x57, 0xBE, 0xAE, 0xD8, 0x34, 0x9F, 0x72, 0xAB, 0x4B, 0x67, 0xC0, 0x9C, 0xD0, 0x7B
        };

        /// <summary>
        /// <see cref="MD5"/> hash of Link OoT model.
        /// </summary>
        public static byte[] LinkOOT = new byte[] {
            0xD1, 0x9A, 0x8F, 0x5A, 0xE8, 0xD1, 0x22, 0x93, 0x8B, 0x73, 0xE5, 0xEB, 0x94, 0x45, 0xC0, 0x97
        };

        /// <summary>
        /// <see cref="MD5"/> hash of Adult Link model.
        /// </summary>
        public static byte[] AdultLink = new byte[] {
            0x0D, 0x34, 0xE3, 0x54, 0xB1, 0xF2, 0xAD, 0x5B, 0x85, 0x01, 0xC3, 0x07, 0xF4, 0x67, 0x1B, 0x23
        };

        /// <summary>
        /// <see cref="MD5"/> hash of Kafei model.
        /// </summary>
        public static byte[] Kafei = new byte[] {
            0x04, 0x4A, 0xD3, 0xBE, 0x08, 0x01, 0x24, 0x31, 0x31, 0x71, 0xF5, 0x12, 0x4B, 0x4F, 0xCD, 0xB4
        };
    }
}
