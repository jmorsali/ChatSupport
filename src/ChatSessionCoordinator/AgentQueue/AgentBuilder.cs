using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ChatSessionCoordinator.AgentQueue;

public class AgentBuilder :IAgentBuilder
{
    private readonly IServiceProvider _provider;

    public AgentBuilder(IServiceProvider provider)
    {
        _provider = provider;
    }
    private readonly List<Agent> agents = new();

    public List<Agent> Build()
    {
        return agents;
    }

    public AgentBuilder AddTeamWithAgent(int Id, string name, int junior, int midLevel, int AgentSenior, int AgentTeamLead, AgentShifts shift)
    {
        var team = new Team { Id = Id, Name = name };
        for (int i = 0; i < junior; i++)
        {
            var agentQueue = _provider.GetService<IAgentQueue>();
            var agent = new Agent(agentQueue ?? throw new InvalidOperationException())
            {
                AgentId = Guid.NewGuid(),
                AgentName = $"Team{team.Name}_Junior{i}",
                AgentLevel = new AgentJuniorLevel(),
                Shift = shift,
                Statuses = AgentStatuses.Available
            };
            agent.AddToTeam(team);
            agents.Add(agent);
        }
        return this;
    }
}