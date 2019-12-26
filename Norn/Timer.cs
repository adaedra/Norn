namespace Norn
{
    public interface ITimer
    {
        bool AutoReset { get; set; }
        double Interval { get; set; }
        bool Enabled { get; set; }
        void Start();
        void Stop();
        event System.Timers.ElapsedEventHandler Elapsed;
    }

    public class Timer : System.Timers.Timer, ITimer
    {
        public Timer(double interval) : base(interval) {}
        public Timer() : base() {}
    }
}
