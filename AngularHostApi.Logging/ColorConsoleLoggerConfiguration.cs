/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.Logging;

using Microsoft.Extensions.Logging;


public class ColorConsoleLoggerConfiguration
{
    public int EventId { get; set; }

    
    public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
    {
        [LogLevel.Information] = ConsoleColor.Green
    };
}
