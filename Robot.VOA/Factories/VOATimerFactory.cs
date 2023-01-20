using Microsoft.Extensions.Logging;
using Robot.Abstractions.Factories;

namespace Robot.VOA.Factories;

internal class VOATimerFactory : ITimerFactory
{
    private readonly ILogger<VOATimerFactory> logger;

    public VOATimerFactory(ILogger<VOATimerFactory> logger)
    {
        this.logger = logger;
    }

    public Timer CreateTimer(TimerCallback callback, int period = 1000)
    {
        return new Timer(callback: callback, state: null, dueTime: Timeout.Infinite, period: period);
    }
}
