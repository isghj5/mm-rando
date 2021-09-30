namespace MMR.Randomizer
{
    public interface IProgressReporter
    {
        void ReportProgress(int percentProgress, string message);
    }
}
