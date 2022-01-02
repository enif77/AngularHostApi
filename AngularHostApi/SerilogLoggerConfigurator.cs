/* AngularHostApi - (C) 2021 - 2022 Premysl Fara  */

namespace AngularHostApi;

using Microsoft.Extensions.Logging;

using Serilog;

using AngularHostApi.AppServer;


public class SerilogLoggerConfigurator : ILoggerConfigurator
{
    /// <summary>
    /// Gets or sets the minimum log level.
    /// </summary>
    public LogLevel MinimalLogLevel { get; set; } = LogLevel.Information;

    
    public ILoggingBuilder Configure(ILoggingBuilder builder)
    {
        builder.ClearProviders();
        
        builder.AddSerilog(dispose: true);
        builder.SetMinimumLevel(MinimalLogLevel);

        return builder;
    }
}