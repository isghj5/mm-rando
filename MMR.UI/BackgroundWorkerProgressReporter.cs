using MMR.Randomizer;
using System.ComponentModel;
using System.Threading;

namespace MMR.UI
{
    public class BackgroundWorkerProgressReporter : IProgressReporter
    {
        private readonly BackgroundWorker _worker;
        public BackgroundWorkerProgressReporter(BackgroundWorker worker)
        {
            _worker = worker;
        }

        public void ReportProgress(int percentProgress, string message, CancellationTokenSource ctsItemImportance)
        {
            _worker.ReportProgress(percentProgress, new BackgroundWorkerProgressState
            {
                Message = message,
                CTSItemImportance = ctsItemImportance
            });
        }
    }

    public class BackgroundWorkerProgressState
    {
        public string Message { get; set; }
        public CancellationTokenSource CTSItemImportance { get; set; }
    }
}
