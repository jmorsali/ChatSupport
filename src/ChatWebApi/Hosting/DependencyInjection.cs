using System.Diagnostics.CodeAnalysis;
using ChatSessionCoordinator.AgentQueue;
using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Models.Enums;

namespace ChatWebApi.Hosting;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static void AddRecordsWriter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAgentBuilder, AgentBuilder>();
        services.AddSingleton<IAgentPool, InMemoryAgentPool>();

        services.AddOptions<SessionCoordinatorConfiguration>()
            .Bind(configuration.GetSection(SessionCoordinatorConfiguration.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}

