namespace GameTimeTracker
{
    public class TimeUpdatedEventArgs : EventArgs
    {
        public TimeUpdatedEventArgs(TimeSpan time)
        {
            Time = time;
        }
        public TimeSpan Time { get; }
    }
}
