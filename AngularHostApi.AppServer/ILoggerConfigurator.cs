/* AngularHostApi - (C) 2021 - 2022 Premysl Fara  */

using Microsoft.Extensions.Logging;

namespace AngularHostApi.AppServer;


/// <summary>
/// Custom logger configurator.
/// </summary>
public interface ILoggerConfigurator
{
    /// <summary>
    /// Configures and adds a custom logger.
    /// </summary>
    /// <param name="builder">An ILoggingBuilder instance.</param>
    /// <returns>The received ILoggingBuilder instance.</returns>
    ILoggingBuilder Configure(ILoggingBuilder builder);
}
