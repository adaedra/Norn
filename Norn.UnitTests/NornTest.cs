using Moq;
using Norn.Test;
using Xunit;

namespace Norn.UnitTests
{
    public interface IInvocable
    {
        void Invoke();
    }

    public class NornTest
    {
        private readonly TestScheduler _scheduler = new TestScheduler();
        private readonly Mock<IInvocable> _mock = new Mock<IInvocable>();

        [Fact]
        public void Test_CanAdvanceTime()
        {
            var timer = _scheduler.CreateTimer(1000);

            timer.Elapsed += (source, args) => _mock.Object.Invoke();
            timer.Start();
            _mock.Verify(mock => mock.Invoke(), Times.Never);

            _scheduler.AdvanceTimeBy(2000);
            _mock.Verify(mock => mock.Invoke(), Times.Once);
        }

        [Fact]
        public void Test_CanAdvanceToNextTimer()
        {
            var closeTimer = _scheduler.CreateTimer(1000);
            var farTimer = _scheduler.CreateTimer(10000);

            closeTimer.Elapsed += (source, args) => _mock.Object.Invoke();
            farTimer.Elapsed += (source, args) => throw new System.Exception("Should not be called");
            closeTimer.Start();
            farTimer.Start();

            _scheduler.AdvanceToNextTimer();
            _mock.Verify(mock => mock.Invoke(), Times.Once);
        }
    }
}
