using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.AgentPool;

public interface IAgentPool
{
    void Initialize(IEnumerable<Agent> agents, Func<int,List<Agent>> KickOfAction);
    public void KickOverflowTeam(int overFlowCount);
    Agent? GetAvailableAgent();
    List<Agent> GetAvailableAgents();
    Task<ActorChat?> GetChatById(Guid chatId);
    public Team? CurrentTeam();
    bool HasOverflow { get; set; }
}