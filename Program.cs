//var builder = WebApplication.CreateBuilder(args);
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

app.UseAuthorization();

app.MapControllers();

// https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs
// https://github.com/aspnet/Hosting/blob/master/src/Microsoft.Extensions.Hosting.Abstractions/HostingAbstractionsHostExtensions.cs
app.Run();

Console.WriteLine("App: DONE!");
