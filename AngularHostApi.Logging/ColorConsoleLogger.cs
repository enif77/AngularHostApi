/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.Logging;

using Microsoft.Extensions.Logging;


/// <summary>
/// https://docs.microsoft.com/en-us/dotnet/core/extensions/custom-logging-provider
/// </summary>
public class ColorConsoleLogger : ILogger
{
    private readonly string _name;
    private readonly Func<ColorConsoleLoggerConfiguration> _getCurrentConfig;

    
    public ColorConsoleLogger(
        string name,
        Func<ColorConsoleLoggerConfiguration> getCurrentConfig) =>
        (_name, _getCurrentConfig) = (name, getCurrentConfig);

    
    public IDisposable BeginScope<TState>(TState state) => default!;

    
    public bool IsEnabled(LogLevel logLevel) =>
        _getCurrentConfig().LogLevels.ContainsKey(logLevel);

    
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var config = _getCurrentConfig();
        if (config.EventId == 0 || config.EventId == eventId.Id)
        {
            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = config.LogLevels[logLevel];
            Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");
            
            Console.ForegroundColor = originalColor;
            Console.WriteLine($"     {_name} - {formatter(state, exception)}");
        }
    }
}