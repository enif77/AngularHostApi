/* AngularHostApi - (C) 2021 Premysl Fara  */

namespace AngularHostApi.Services
{
    using AngularHostApi.Logging;
    
    
    public class AppServerService
    {
        public IAppServer CreateAppServer(string[] args)
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

            // app.UseAuthorization();

            app.MapControllers();
            
            return new AppServer(
                app,
                //app.Services.GetRequiredService<ILogger<AppServerService>>()
                app.Logger);
        }
    }    
}

/*
 
https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-6.0
  
 */
 