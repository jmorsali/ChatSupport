using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.AgentQueue;

public interface IAgentBuilder
{
    AgentBuilder AddTeamWithAgent(int Id, string name, int junior, int midLevel, int senior, int teamLead, AgentShifts shift);
    List<Agent> KickOverflowTeam(int configurationOverFlowCount);
}