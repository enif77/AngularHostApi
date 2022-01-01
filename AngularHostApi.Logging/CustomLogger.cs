/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.Logging;

using Microsoft.Extensions.Logging;


public class CustomLogger : ILogger
{
    private readonly ILogger _logger;


    public CustomLogger(ILogger logger)
    {
        _logger = logger;
    }


    public IDisposable BeginScope<TState>(TState state) => _logger.BeginScope(state);

    
    public bool IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);

    
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        _logger.Log(logLevel, eventId, state, exception, formatter);
    }
}