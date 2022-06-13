using MMR.Randomizer;
using System.IO;
using System.Threading;

namespace MMR.CLI
{
    partial class Program
    {
        public class TextWriterProgressReporter : IProgressReporter
        {
            private readonly TextWriter _textWriter;

            public TextWriterProgressReporter(TextWriter textWriter)
            {
                _textWriter = textWriter;
            }

            public void ReportProgress(int percentProgress, string message, CancellationTokenSource ctsItemImportance)
            {
                _textWriter.WriteLine(message);
            }
        }
    }
}
