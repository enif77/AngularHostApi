/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.Logging;

using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;


public sealed class CustomLoggerProvider : ILoggerProvider
{
    private readonly ILogger _logger;
    private readonly ConcurrentDictionary<string, ILogger> _loggers = new();

    
    public CustomLoggerProvider(ILogger logger)
    {
        _logger = logger;
    }

    
    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => _logger);

    
    public void Dispose()
    {
        _loggers.Clear();
    }
}