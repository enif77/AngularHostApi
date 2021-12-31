/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.AppServer;

public interface IAppServer
{
    Task StartAsync(CancellationTokenSource? tokenSource = null);
    Task<TaskStatus> StopAsync();
}