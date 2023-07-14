using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.AgentQueue;

public interface IAgentPool
{
    void Initialize(IEnumerable<Agent> agents);
}
public class InMemoryAgentPool : IAgentPool
{
    public List<Agent> Agents { get; private set; } = new();

    public void Initialize(IEnumerable<Agent> agents)
    {
        Agents = agents.ToList();
    }
}