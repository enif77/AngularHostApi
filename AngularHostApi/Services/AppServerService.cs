/* AngularHostApi - (C) 2021 Premysl Fara  */

using AngularHostApi.Logging;

namespace AngularHostApi.Services
{
    public class AppServerService
    {
        private Task? _appServerTask;
        private CancellationTokenSource? _tokenSource;
        private bool _tokenSourceCanBeDisposed;
        
        
        public async Task<bool> StartAppServerAsync(string[] args, CancellationTokenSource? tokenSource = null)
        {
            if (_appServerTask != null) throw new InvalidOperationException("The app server is already running.");

            if (tokenSource == null)
            {
                _tokenSource = new CancellationTokenSource();
                _tokenSourceCanBeDisposed = true;
            }
            else
            {
                _tokenSource = tokenSource;
            }

            var app = CreateAppServer(args);
            
            // https://docs.microsoft.com/cs-cz/dotnet/api/system.threading.tasks.task.run?view=net-6.0
            //var tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;

            var t = Task.Run(async () => 
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }

                // https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs
                // https://github.com/aspnet/Hosting/blob/master/src/Microsoft.Extensions.Hosting.Abstractions/HostingAbstractionsHostExtensions.cs
                await app.RunAsync(token);
                
                Console.WriteLine("The app server stopped.");
            }, token);

            await Task.Yield();

            _appServerTask = t;

            return true;
        }


        public async Task<TaskStatus> StopAppServerAsync()
        {
            if (_appServerTask == null) throw new InvalidOperationException("The app server is not running.");
            if (_tokenSource == null) throw new InvalidOperationException("The cancellation token source is null.");
            
            _tokenSource.Cancel();

            try
            {
                await _appServerTask;
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Exception messages:");

                foreach (var ie in e.InnerExceptions)
                {
                    Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);
                }
            }
            finally
            {
                if (_tokenSourceCanBeDisposed)
                {
                    _tokenSource.Dispose();
                }
            }

            var status = _appServerTask.Status;

            _appServerTask = null;
            _tokenSource = null;
            
            return status;
        }
        
        
        private WebApplication CreateAppServer(string[] args)
        {
            Console.WriteLine("Creating the app server...");
            
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

            return app;
        }
    }    
}

