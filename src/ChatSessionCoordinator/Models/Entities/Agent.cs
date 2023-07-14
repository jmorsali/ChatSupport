using ChatSessionCoordinator.AgentQueue;
using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.Entities;

public class Agent
{
    public Agent(IAgentQueue agentQueue)
    {
        agentQueue.Agent = this;
        Queue = agentQueue;
    }
    public required Guid AgentId { get; set; }
    public Team? Team { get; private set; }
    public required string AgentName { get; set; }
    public string AgentDescription { get; set; } = string.Empty;
    public required AgentLevel AgentLevel { get; set; }
    public required AgentShifts? Shift { get; set; }
    public AgentStatuses Statuses { get; set; }
    public IAgentQueue Queue { get; }
    public DateTime LastAssignment { get; set; }= DateTime.MinValue;


    public void AddToTeam(Team? newTeam)
    {
        if (Team != null && Team != newTeam) Team.Remove(this);
        if (newTeam != null && newTeam.Agents.Contains(this)) return;

        Team = newTeam;
        Team?.Add(this);
    }
}