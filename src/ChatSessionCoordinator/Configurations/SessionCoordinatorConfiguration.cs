using System.ComponentModel.DataAnnotations;

namespace ChatSessionCoordinator.Configurations;

public class SessionCoordinatorConfiguration
{
    public static readonly string SectionName = "Coordinatior";

    [Required]
    [Range(10, 100)]
    public int MainQueueSize { get; set; }

    [Range(5, 35)]
    public int MaxRetry { get; set; }

}

