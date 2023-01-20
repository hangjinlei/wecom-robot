namespace Robot.Abstractions.Services;

public interface ICountdownService
{
    public void Countdown(Timer timer, int dueTime = 1000, int period = 1000);
}
