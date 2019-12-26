# Norn

Control time in your tests.

Norn allows to control timers execution while in test mode. It provides standard interfaces for a scheduler and timers,
so you can easily switch between implementations between real mode and test mode.

## Example

Let's say you have this bit of code:

```c#
public void PlantBomb() {
    var timer = new Timer(10000);
    timer.Elapsed += (source, args) => Bomb.Boom();
}
```

In this setup, it is not possible to test this easily, except waiting for the timer to resolve. With Norn, you have
to transform code this way:

```c#
using Norn;

public void PlantBomb(IScheduler scheduler) {
    var timer = scheduler.CreateTimer(10000);
    timer.Elapsed += (source, args) => Bomb.Boom();
}
```

In the main program, use the standard Scheduler that will use `System.Timers.Timer` and work as normal:

```c#
using Norn;

public void Main() {
    var scheduler = new Scheduler();
    new Bomb().PlantBomb(scheduler);
}
```

However, in tests, you can use `Norn.Test` to fake timer implementation and control time:

```c#
using Norn;

[Fact]
public void Test_Bomb() {
    var scheduler = new TestScheduler();
    var bomb = new Bomb();

    Assert.False(Bomb.Exploded);
    scheduler.AdvanceToNextTimer(); // Will find the nearest timer, and execute all timers at that time
    Assert.True(Bomb.Exploded);
}
```

## Caveats

* This is a test project to help me with other projects, not a final product yet. The library is very simple and does
  not cover much for now.
