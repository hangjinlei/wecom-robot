using Microsoft.Extensions.Logging;
using Robot.Abstractions.Factories;

namespace Robot.CDT.Factories;

internal class CDTTimerFactory : ITimerFactory
{
    private readonly ILogger<CDTTimerFactory> logger;

    public CDTTimerFactory(ILogger<CDTTimerFactory> logger)
    {
        this.logger = logger;
    }

    public Timer CreateTimer(TimerCallback callback, int period = 1000)
    {
        return new Timer(callback: callback, state: null, dueTime: Timeout.Infinite, period: period);
    }
}
