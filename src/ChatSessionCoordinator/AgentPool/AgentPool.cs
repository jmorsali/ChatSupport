using ChatSessionCoordinator.Extension;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.AgentPool;

public class AgentPool : IAgentPool
{
    private List<Agent> Agents { get; set; } = new();
    private Func<int, List<Agent>>? _kickOfAction;
    public void Initialize(IEnumerable<Agent> agents, Func<int, List<Agent>> KickOfAction)
    {
        Agents = agents.ToList();
        _kickOfAction = KickOfAction;
    }

    public void KickOverflowTeam(int overFlowCount)
    {
        if (_kickOfAction == null) return;

        var agents = _kickOfAction.Invoke(overFlowCount);
        agents.ForEach(a => a.Shift = DateTime.Now.CurrentShift());
        agents.ForEach(a => a.IsOverflow = true);
        agents.ForEach(a => a.AddToTeam(CurrentTeam()));
        Agents.AddRange(agents);

        HasOverflow = true;
    }
    public Agent? GetAvailableAgent()
    {
        foreach (var level in Enum.GetValues(typeof(AgentLevels)))
        {
            var levelAvailable = Agents
                .Where(a =>
                        a.AgentLevel.Level == (AgentLevels)level &&
                        a.Statuses == AgentStatuses.Available &&
                        a.Shift.IsActive() &&
                        a.Queue.ItemCount < a.Team?.MaximumQueueLenght
                    )
                   .MinBy(x => x.LastAssignment);

            if (levelAvailable != null)
                return levelAvailable;
        }
        return null;
    }

    public List<Agent> GetAvailableAgents()
    {
        foreach (var level in Enum.GetValues(typeof(AgentLevels)))
        {
            var levelAvailable = Agents
                .Where(a =>
                    a.AgentLevel.Level == (AgentLevels)level &&
                    a.Statuses == AgentStatuses.Available &&
                    a.Shift.IsActive() &&
                    a.Queue.ItemCount < a.Team?.MaximumQueueLenght
                )
                .OrderBy(x => x.LastAssignment).ToList();

            return levelAvailable;
        }
        return new List<Agent>();
    }

    public Team? CurrentTeam()
    {
        var currentShift = DateTime.Now.CurrentShift();
        return Agents.FirstOrDefault(a => a.Shift == currentShift)?.Team;
    }

    public async Task<ActorChat?> GetChatById(Guid chatId)
    {
        foreach (var agent in Agents)
        {
            var chat = await agent.Queue.GetChatById(chatId);
            if (chat != null) return chat;
        }
        return null;
    }

    public bool HasOverflow { get; set; }
}