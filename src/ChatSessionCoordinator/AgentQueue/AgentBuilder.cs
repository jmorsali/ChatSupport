using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ChatSessionCoordinator.AgentQueue;

public class AgentBuilder :IAgentBuilder
{
    private readonly IServiceProvider _provider;
    private readonly SessionCoordinatorConfiguration _configuration;

    public AgentBuilder(IServiceProvider provider,SessionCoordinatorConfiguration configuration)
    {
        _provider = provider;
        _configuration = configuration;
    }
    private readonly List<Agent> agents = new();

    public List<Agent> Build()
    {
        return agents;
    }

    public AgentBuilder AddTeamWithAgent(int Id, string name, int junior, int midLevel, int senior, int teamLead, AgentShifts shift)
    {
        var team = new Team(_configuration) { Id = Id, Name = name };

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

        for (int i = 0; i < midLevel; i++)
        {
            var agentQueue = _provider.GetService<IAgentQueue>();
            var agent = new Agent(agentQueue ?? throw new InvalidOperationException())
            {
                AgentId = Guid.NewGuid(),
                AgentName = $"Team{team.Name}_Mid{i}",
                AgentLevel = new AgentMidLevel(),
                Shift = shift,
                Statuses = AgentStatuses.Available
            };
            agent.AddToTeam(team);
            agents.Add(agent);
        }

        for (int i = 0; i < senior; i++)
        {
            var agentQueue = _provider.GetService<IAgentQueue>();
            var agent = new Agent(agentQueue ?? throw new InvalidOperationException())
            {
                AgentId = Guid.NewGuid(),
                AgentName = $"Team{team.Name}_Senior{i}",
                AgentLevel = new AgentSenior(),
                Shift = shift,
                Statuses = AgentStatuses.Available
            };
            agent.AddToTeam(team);
            agents.Add(agent);
        }

        for (int i = 0; i < teamLead; i++)
        {
            var agentQueue = _provider.GetService<IAgentQueue>();
            var agent = new Agent(agentQueue ?? throw new InvalidOperationException())
            {
                AgentId = Guid.NewGuid(),
                AgentName = $"Team{team.Name}_TeamLead{i}",
                AgentLevel = new AgentTeamLead(),
                Shift = shift,
                Statuses = AgentStatuses.Available
            };
            agent.AddToTeam(team);
            agents.Add(agent);
        }
        return this;
    }

    public List<Agent> KickOverflowTeam(int configurationOverFlowCount)
    {
        List<Agent> overflowAgents = new List<Agent>();
        for (int i = 0; i < configurationOverFlowCount; i++)
        {
            var agentQueue = _provider.GetService<IAgentQueue>();
            var agent = new Agent(agentQueue ?? throw new InvalidOperationException())
            {
                AgentId = Guid.NewGuid(),
                AgentName = $"Team_Overflow_Junior_Overflow_{i}",
                AgentLevel = new AgentJuniorLevel(),
                Shift = null,
                Statuses = AgentStatuses.Available
            };
            overflowAgents.Add(agent);
        }

        return overflowAgents;
    }
}