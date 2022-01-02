/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.AppServer;

using System.Reflection;


public class AppServerOptions
{
    /// <summary>
    /// Application args.
    /// </summary>
    public string[] Args { get; set; } = Array.Empty<string>();

    /// <summary>
    /// A path to a dist directory of an Angular app.
    /// </summary>
    public string WebRootPath { get; set; } = string.Empty;
    
    /// <summary>
    /// The list of assemblies containing controllers.
    /// </summary>
    public IList<Assembly> AssembliesWithControllers { get; } = new List<Assembly>();

    /// <summary>
    /// An optional custom logging configurator.
    /// </summary>
    public ILoggerConfigurator? LoggerConfigurator { get; set; }
}
