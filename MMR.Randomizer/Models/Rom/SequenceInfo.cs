using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using MMR.Randomizer.Constants;

namespace MMR.Randomizer.Models.Rom
{

    public class SequenceInfo
    {
        public SequenceInfo SequenceCopy()
        {
            return new SequenceInfo
            {
                Name                = this.Name,
                Directory           = this.Directory,
                Replaces            = this.Replaces,
                MM_seq              = this.MM_seq,
                Type                = this.Type,
                Instrument          = this.Instrument,
                SequenceBinaryList  = this.SequenceBinaryList,
                PreviousSlot        = this.PreviousSlot,
                InstrumentSamples   = this.InstrumentSamples,
            };
        }

        public string Name { get; set; }
        public string Directory { get; set; } = Values.MusicDirectory;
        public string Filename => Path.Combine(Directory, Name);
        public int Replaces { get; set; } = -1;
        public int MM_seq { get; set; } = -1;
        public List<int> Type { get; set; } = new List<int>();
        public int Instrument { get; set; }
        public List<SequenceBinaryData> SequenceBinaryList { get; set; }
        public int PreviousSlot { get; set; } = -1;
        public List<SequenceSoundSampleBinaryData> InstrumentSamples { get; set; } = null;
    }
}
