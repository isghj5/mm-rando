using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Symbols used in the ASM patch.
    /// </summary>
    public class AsmSymbols
    {
        private Dictionary<string, uint> _symbols = new Dictionary<string, uint>();

        /// <summary>
        /// Address of payload end.
        /// </summary>
        public uint PayloadEnd => this["PAYLOAD_END"];

        /// <summary>
        /// Address of payload start.
        /// </summary>
        public uint PayloadStart => this["PAYLOAD_START"];

        /// <summary>
        /// Get the value of a symbol by name.
        /// </summary>
        /// <param name="name">Symbol name</param>
        /// <returns>Symbol value</returns>
        public uint this[string name] => _symbols[name];

        /// <summary>
        /// Get the offset of a symbol value relative to payload start address.
        /// </summary>
        /// <param name="name">Symbol name</param>
        /// <returns></returns>
        public uint Offset(string name) => _symbols[name] - PayloadStart;

        private AsmSymbols()
            : this(new Dictionary<string, uint>())
        {
        }

        private AsmSymbols(Dictionary<string, uint> symbols)
        {
            this._symbols = symbols;
        }

        /// <summary>
        /// Check if a certain symbol exists.
        /// </summary>
        /// <param name="name">Symbol name</param>
        /// <returns></returns>
        public bool Has(string name)
        {
            return this._symbols.ContainsKey(name);
        }

        /// <summary>
        /// Load <see cref="AsmSymbols"/> from a JSON <see cref="string"/>.
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <returns></returns>
        public static AsmSymbols FromJSON(string json)
        {
            var result = JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                .Select(x => new KeyValuePair<string, uint>(x.Key, Convert.ToUInt32(x.Value, 16)))
                .ToDictionary(x => x.Key, x => x.Value);
            return new AsmSymbols(result);
        }

        /// <summary>
        /// Load <see cref="AsmSymbols"/> from the default resource file.
        /// </summary>
        /// <returns></returns>
        public static AsmSymbols Load()
        {
            return FromJSON(Resources.asm.symbols);
        }
    }
}
