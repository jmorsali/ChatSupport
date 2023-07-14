using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.AgentQueue;

public interface IAgentBuilder
{
    AgentBuilder AddTeamWithAgent(int Id, string name, int junior, int midLevel, int AgentSenior, int AgentTeamLead, AgentShifts shift);
}