using System.Diagnostics.CodeAnalysis;
using ChatSessionCoordinator.Configurations;

namespace ChatWebApi.Hosting;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static void AddRecordsWriter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<SessionCoordinatorConfiguration>()
            .Bind(configuration.GetSection(SessionCoordinatorConfiguration.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}

