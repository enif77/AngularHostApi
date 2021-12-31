/* AngularHostApi - (C) 2021 Premysl Fara  */

using AngularHostApi.AppServer;
using AngularHostApi.Logging;


// Console.WriteLine("The app is running. Press ENTER to start the app server...");
// _ = Console.ReadLine();  // This represents a running application (the launcher) before it runs the app itself.

ILogger logger;

var appServer = BuildAppServer(args);

logger.LogInformation("Starting the app server...");

await appServer.StartAsync();

logger.LogInformation("The app server is started. Press ENTER to stop the app server...");

_ = Console.ReadLine();  // This represents a running application (the launcher).

logger.LogInformation("Stopping the app server...");
    
var status = await appServer.StopAsync();
    
logger.LogInformation("Task status: {Status}", status);

logger.LogInformation("DONE!");

// ---

IAppServer BuildAppServer(string[] args)
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args,
            
        // A path to a dist directory of an Angular app.
        WebRootPath = "/home/enif/Devel/Projects/Web/Angular/my-app/dist/my-app/"    
    });

    builder.Logging.ClearProviders();
    //builder.Logging.AddConsole();
    builder.Logging.AddColorConsoleLogger(configuration =>
    {
        configuration.LogLevels.Add(LogLevel.Warning, ConsoleColor.DarkMagenta);
        configuration.LogLevels.Add(LogLevel.Error, ConsoleColor.Red);
    });

    // Add services to the container.

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    // This removes the CTRL+C app shutdown.
    builder.Services.AddSingleton<IHostLifetime, NoopConsoleLifetime>();

    var app = builder.Build();
            
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseAuthorization();

    app.MapControllers();

    logger = app.Logger;
    
    return new AppServer(app, app.Logger);
}

/*
 
https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0
  
 */
 