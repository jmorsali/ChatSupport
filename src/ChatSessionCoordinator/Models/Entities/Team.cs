using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.Entities;

public class Team
{
    private readonly SessionCoordinatorConfiguration _configuration;

    public Team(SessionCoordinatorConfiguration configuration)
    {
        _configuration = configuration;
    }
    public required int Id { get; set; }
    public required string Name { get; set; }
    public List<Agent> Agents { get; } = new();
    public double Capacity {
        get
        {
            var availableAgents = Agents.Where(a => a.Statuses == AgentStatuses.Available);
            double capacity = 0;
            foreach (var a in availableAgents)
            {
                capacity += a.AgentLevel.SeniorityMultipliers * _configuration.MaxAgentConcurrency;
            }

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

