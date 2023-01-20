namespace Robot.Abstractions.Factories;

public interface ITimerFactory
{
    public Timer CreateTimer(TimerCallback callback, int period = 1000);
}
