using System.Threading;

namespace MMR.Randomizer.Tests
{
    internal class NoProgressReporter : IProgressReporter
    {
        public void ReportProgress(int percentProgress, string message, CancellationTokenSource ctsItemImportance = null)
        {
        }
    }
}
