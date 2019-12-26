using System;
using System.Collections.Generic;
using System.Linq;

namespace Norn.Test
{
    public class TestScheduler : IScheduler
    {
        public TestScheduler() { }

        private readonly List<TestTimer> _timers = new List<TestTimer>();

        public ITimer CreateTimer(double interval, bool autoReset = false) => new TestTimer(interval) { AutoReset = autoReset };
        public ITimer CreateTimer(bool autoReset = false) => CreateTimer(0, autoReset);

        public void AdvanceToNextTimer()
        {
            var nextTimer = _timers.Where(timer => timer.Enabled).OrderBy(timer => timer.RemainingTime).FirstOrDefault();
            if (nextTimer != null)
            {
                AdvanceTimeBy(nextTimer.RemainingTime);
            }
        }

        public void AdvanceTimeBy(double interval)
        {
            if (interval < 0) { throw new ArgumentException("Interval can't be negative", "interval"); }

            _timers.ForEach(timer => timer.RemainingTime -= interval);
            foreach (var timer in _timers.Where(timer => timer.Enabled && timer.RemainingTime <= 0))
            {
                timer.FireNow();
            }
        }
    }
}
