using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Models.Enums;
using Microsoft.Extensions.Options;

namespace ChatSessionCoordinator.Models.Entities;

public class Team
{
    private readonly SessionCoordinatorConfiguration _configuration;

    public Team(IOptions<SessionCoordinatorConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }
    public required int Id { get; set; }
    public required string Name { get; init; }
    public List<Agent> Agents { get; } = new();

    private double Capacity
    {
        get
        {
            var availableAgents = Agents.Where(a => a is { Statuses: AgentStatuses.Available });
            double capacity = 0;
            foreach (var a in availableAgents)
                capacity += a.AgentLevel.SeniorityMultipliers * _configuration.MaxAgentConcurrency;

            return capacity;
        }
    }

    public double MaximumQueueLenght => Capacity * 1.5;

    public void Remove(Agent agent)
    {
        Agents.Remove(agent);
    }
    public void Add(Agent agent)
    {
        Agents.Add(agent);
    }
}

