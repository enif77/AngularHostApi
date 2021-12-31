namespace AngularHostApi.Services;

public interface IAppServer
{
    Task<bool> StartAsync(CancellationTokenSource? tokenSource = null);
    Task<TaskStatus> StopAsync();
}