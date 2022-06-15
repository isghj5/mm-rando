using MMR.Randomizer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MMR.CLI
{
    partial class Program
    {
        public class ProgressBarProgressReporter : IProgressReporter
        {
            private readonly ProgressBar _progressBar;
            private readonly int? _maxImportanceWait;

            public ProgressBarProgressReporter(ProgressBar progressBar, int? maxImportanceWait)
            {
                _progressBar = progressBar;
                _maxImportanceWait = maxImportanceWait;
            }

            public void ReportProgress(int percentProgress, string message, CancellationTokenSource ctsItemImportance)
            {
                //_progressBar.WriteLine(message);
                if (ctsItemImportance != null && _maxImportanceWait.HasValue)
                {
                    ctsItemImportance.CancelAfter(TimeSpan.FromSeconds(_maxImportanceWait.Value));
                }
                _progressBar.Report(new Tuple<double, string>(percentProgress / 100.0, message));
            }
        }
    }
}
