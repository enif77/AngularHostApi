using AngularHostApi.Services;


var appServerService = new AppServerService();

Console.WriteLine("Starting the app server...");

_ = await appServerService.StartAppServerAsync(args);

Console.WriteLine("The app server is started.");

_ = Console.ReadLine();  // This represents a running application (the launcher).

Console.WriteLine("Stopping the app server...");
    
var status = await appServerService.StopAppServerAsync();
    
Console.WriteLine("\nTask status: {0}", status);

Console.WriteLine("DONE!");
