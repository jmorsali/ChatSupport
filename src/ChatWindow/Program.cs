using ChatWindow;
using Microsoft.Extensions.DependencyInjection;


ServiceProvider serviceProvider = Startup.RegisterServices(args);

var runner = new ClientRuner();
var tasks=  runner.StartAsync(serviceProvider);
Task.WaitAll(tasks);


serviceProvider.Dispose();