namespace Norn.Test
{
    public class TestTimer : ITimer
    {
        public TestTimer(double interval = 0) { Interval = interval; }
        public void Start()
        {
            Enabled = true;
            RemainingTime = Interval;
        }

        public void Stop()
        {
            Enabled = false;
        }

        public bool Enabled { get; set; }
        public bool AutoReset { get; set; }
        public double Interval { get; set; }
        public event System.Timers.ElapsedEventHandler Elapsed;

        internal double RemainingTime { get; set; }

        public void FireNow()
        {
            if (Enabled)
            {
                // TODO: Use a correct second argument
                Elapsed?.Invoke(this, null);
                if (!AutoReset) { Enabled = false; }
                RemainingTime = Interval;
            }
        }
    }
}
