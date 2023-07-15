using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatWindow;

internal class Startup
{
    private static IConfiguration SetupConfiguration(string[] args)
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();
    }

    internal static ServiceProvider RegisterServices(string[] args)
    {
        IConfiguration configuration = SetupConfiguration(args);
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(cfg => cfg.AddConsole());
        serviceCollection.AddSingleton(configuration);

        serviceCollection.AddHttpClient<IChatApiClient, ChatApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["BaseUrl"]);
        });


        return serviceCollection.BuildServiceProvider();
    }
}

