/* AngularHostApi - (C) 2021 - 2022 Premysl Fara  */

namespace AngularHostApi;

using Microsoft.Extensions.Logging;

using AngularHostApi.AppServer;
using AngularHostApi.Logging;


public class ColorConsoleLoggerConfigurator : ILoggerConfigurator
{
    /// <summary>
    /// Gets or sets the minimum log level.
    /// </summary>
    public LogLevel MinimalLogLevel { get; set; } = LogLevel.Information;

    
    public ILoggingBuilder Configure(ILoggingBuilder builder)
    {
        builder.ClearProviders();
        
        builder.AddColorConsoleLogger(configuration =>
        {
            configuration.LogLevels.Add(LogLevel.Debug, ConsoleColor.Gray);
            configuration.LogLevels.Add(LogLevel.Warning, ConsoleColor.DarkMagenta);
            configuration.LogLevels.Add(LogLevel.Error, ConsoleColor.Red);
        });
        builder.SetMinimumLevel(MinimalLogLevel);

        return builder;
    }
}