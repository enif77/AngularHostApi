/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.Logging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;


public static class CustomLoggerExtensions
{
    public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder builder, ILogger logger)
    {
        builder.AddConfiguration();

        builder.Services.TryAddSingleton(logger);
        
        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());

        return builder;
    }
}