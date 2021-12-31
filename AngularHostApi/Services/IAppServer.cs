namespace AngularHostApi.Services;

public interface IAppServer
{
    Task StartAsync(CancellationTokenSource? tokenSource = null);
    Task<TaskStatus> StopAsync();
}