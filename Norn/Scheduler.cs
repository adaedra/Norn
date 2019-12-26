namespace Norn
{
    public interface IScheduler
    {
        ITimer CreateTimer(double interval, bool autoReset = false);
        ITimer CreateTimer(bool autoReset = false);
    }

    public class Scheduler : IScheduler
    {
        public ITimer CreateTimer(double interval, bool autoReset = false) => new Timer(interval) { AutoReset = autoReset };
        public ITimer CreateTimer(bool autoReset = false) => new Timer() { AutoReset = autoReset };
    }
}
