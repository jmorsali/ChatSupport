using System.ComponentModel.DataAnnotations;

namespace ChatSessionCoordinator.Configurations;

public class SessionCoordinatorConfiguration
{
    public static readonly string SectionName = "Coordinatior";

    [Required]
    [Range(5, 50)]
    public int MainQueueSize { get; set; }

    [Required]
    [Range(5, 20)]
    public int MaxAgentConcurrency { get; set; }

    [Required]
    [Range(1, 10)]
    public int OverFlowCount { get; set; }
}

