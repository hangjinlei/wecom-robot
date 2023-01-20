using Microsoft.Extensions.Logging;
using Robot.Abstractions.Services;

namespace Robot.VOA.Services;

internal class VOACountdownService : ICountdownService
{
    private readonly ILogger<VOACountdownService> logger;

    public VOACountdownService(ILogger<VOACountdownService> logger)
    {
        this.logger = logger;
    }

    public void Countdown(Timer timer, int dueTime = 1000, int period = 1000)
    {
        timer.Change(dueTime, period);
    }
}
