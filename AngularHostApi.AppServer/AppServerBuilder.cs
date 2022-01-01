/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.AppServer;

using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using AngularHostApi.Logging;


public static class AppServerBuilder
{
    public static IAppServer Build(AppServerOptions? options = null)
    {
        options ??= new AppServerOptions();

        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            Args = options.Args,
            WebRootPath = string.IsNullOrWhiteSpace(options.WebRootPath)
                ? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
                : options.WebRootPath
        });

        builder.Logging.ClearProviders();
        //builder.Logging.AddConsole();
        builder.Logging.AddColorConsoleLogger(configuration =>
        {
            configuration.LogLevels.Add(LogLevel.Warning, ConsoleColor.DarkMagenta);
            configuration.LogLevels.Add(LogLevel.Error, ConsoleColor.Red);
        });

        // Add services to the container.

        var mvcBuilder = builder.Services.AddControllers()
            .AddApplicationPart(typeof(AppServerBuilder).Assembly); // This adds controllers from this assembly.

        // This adds controllers from this user defined assemblies.
        foreach (var assembly in options.AssembliesWithControllers)
        {
            mvcBuilder.AddApplicationPart(assembly);
        }
        
        
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

        //logger = app.Logger;
    
        return new AppServer(app, app.Logger);
    }
}