using Robot.Abstractions;

namespace Robot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEnumerable<IRobotService> robotServices;

    public Worker(ILogger<Worker> logger, IEnumerable<IRobotService> robotServices)
    {
        _logger = logger;
        this.robotServices = robotServices;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach (var robotService in robotServices)
        {
            robotService.Run();
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}