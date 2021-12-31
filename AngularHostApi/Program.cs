using AngularHostApi.Services;

Console.WriteLine("The app is running. Press ENTER to start the app server...");

_ = Console.ReadLine();  // This represents a running application (the launcher) before it runs the app itself.


var appServerService = new AppServerService();

var appServer = appServerService.CreateAppServer(args);

Console.WriteLine("Starting the app server...");

await appServer.StartAsync();

Console.WriteLine("The app server is started. Press ENTER to stop the app server...");

_ = Console.ReadLine();  // This represents a running application (the launcher).

Console.WriteLine("Stopping the app server...");
    
var status = await appServer.StopAsync();
    
Console.WriteLine("\nTask status: {0}", status);

Console.WriteLine("DONE!");
