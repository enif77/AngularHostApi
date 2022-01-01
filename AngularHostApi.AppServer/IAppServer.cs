/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.AppServer;

using Microsoft.Extensions.Logging;


public interface IAppServer
{
    ILogger Logger { get; }
    Task StartAsync(CancellationTokenSource? tokenSource = null);
    Task<TaskStatus> StopAsync();
}