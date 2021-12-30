Console.WriteLine("Creating the app server...");

var app = CreateAppServer(args);

Console.WriteLine("Starting the app server...");

var tokenSource = new CancellationTokenSource();
var t = await StartAppServerAsync(app, tokenSource);

Console.WriteLine("The app server is started.");

_ = Console.ReadLine();  // This represents a running application (the launcher).

try
{
    Console.WriteLine("Stopping the app server...");
    
    var status = await StopAppServerAsync(t, tokenSource);
    
    Console.WriteLine("\nTask status: {0}", status);
}
finally
{
    tokenSource.Dispose();
}

Console.WriteLine("DONE!");

// -------------

static WebApplication CreateAppServer(string[] args)
{
    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args,

        // A path to a dist directory of an Angular app.
        WebRootPath = "/home/enif/Devel/Projects/Web/Angular/my-app/dist/my-app/"    
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


static async Task<Task> StartAppServerAsync(WebApplication app, CancellationTokenSource tokenSource)
{
    // https://docs.microsoft.com/cs-cz/dotnet/api/system.threading.tasks.task.run?view=net-6.0
    //var tokenSource = new CancellationTokenSource();
    var token = tokenSource.Token;

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

    return t;
}


static async Task<TaskStatus> StopAppServerAsync(Task t, CancellationTokenSource tokenSource)
{
    tokenSource.Cancel();

    try
    {
        await t;
    }
    catch (AggregateException e)
    {
        Console.WriteLine("Exception messages:");
        
        foreach (var ie in e.InnerExceptions)
        {
            Console.WriteLine("   {0}: {1}", ie.GetType().Name, ie.Message);
        }
    }

    return t.Status;
}