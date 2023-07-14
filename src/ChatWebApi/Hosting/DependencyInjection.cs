using System.Diagnostics.CodeAnalysis;
using ChatSessionCoordinator.AgentPool;
using ChatSessionCoordinator.AgentQueue;
using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Coordinator;
using ChatSessionCoordinator.SessionQueue;

namespace ChatWebApi.Hosting;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static void AddCoreDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<ChatSessionCoordinatioBackGroundService>();

        services.AddSingleton<IAgentBuilder, AgentBuilder>();
        services.AddSingleton<IAgentPool, AgentPool>();
        services.AddSingleton<IAgentQueue, InMemoryAgentQueue>();
        services.AddSingleton<ISessionCoordinator, SessionCoordinator>();
        services.AddSingleton<ISessionQueue, InMemorySessionQueue>();

        services.AddOptions<SessionCoordinatorConfiguration>()
            .Bind(configuration.GetSection(SessionCoordinatorConfiguration.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}

