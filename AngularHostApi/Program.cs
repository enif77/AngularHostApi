using AngularHostApi.Services;


var appServerService = new AppServerService();

var appServer = appServerService.CreateAppServer(args);

Console.WriteLine("Starting the app server...");

_ = await appServer.StartAsync();

Console.WriteLine("The app server is started.");

_ = Console.ReadLine();  // This represents a running application (the launcher).

Console.WriteLine("Stopping the app server...");
    
var status = await appServer.StopAsync();
    
Console.WriteLine("\nTask status: {0}", status);

Console.WriteLine("DONE!");
