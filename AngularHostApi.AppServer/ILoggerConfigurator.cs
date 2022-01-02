/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.AppServer;

using Microsoft.Extensions.Hosting;


/// <summary>
/// Custom logger configurator.
/// </summary>
public interface ILoggerConfigurator
{
    /// <summary>
    /// Configures a custom logger.
    /// </summary>
    /// <param name="builder">An ILoggingBuilder instance.</param>
    /// <returns>The received ILoggingBuilder instance.</returns>
    IHostBuilder ConfigureLogger(IHostBuilder builder);
}
