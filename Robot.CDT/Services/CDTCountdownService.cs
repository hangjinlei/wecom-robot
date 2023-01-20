using Microsoft.Extensions.Logging;
using Robot.Abstractions.Services;

namespace Robot.CDT.Services;

internal class CDTCountdownService : ICountdownService
{
    private readonly ILogger<CDTCountdownService> logger;

    public CDTCountdownService(ILogger<CDTCountdownService> logger)
    {
        this.logger = logger;
    }

    public void Countdown(Timer timer, int dueTime = 1000, int period = 1000)
    {
        timer.Change(dueTime, period);
    }
}
