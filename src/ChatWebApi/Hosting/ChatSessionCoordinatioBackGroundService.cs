using ChatSessionCoordinator.AgentPool;
using ChatSessionCoordinator.AgentQueue;
using ChatSessionCoordinator.Coordinator;
using ChatSessionCoordinator.Models.Enums;

namespace ChatWebApi.Hosting;

public class ChatSessionCoordinatioBackGroundService : BackgroundService
{
    private readonly ISessionCoordinator _sessionCoordinator;
    private readonly IAgentPool _agentPool;
    private readonly IAgentBuilder _agentBuilder;
   
    public ChatSessionCoordinatioBackGroundService(
        ISessionCoordinator sessionCoordinator,
        IAgentPool agentPool,IAgentBuilder agentBuilder
    )
    {
        _sessionCoordinator = sessionCoordinator;
        _agentPool = agentPool;
        _agentBuilder = agentBuilder;
       
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var agents = _agentBuilder
            .AddTeamWithAgent(1, " Team A", junior: 7, midLevel: 4, senior: 2, teamLead: 1,
                shift: AgentShifts.From8_To16)
           .AddTeamWithAgent(2, " Team B", junior: 5, midLevel: 2,  senior: 1, teamLead: 1,
                shift: AgentShifts.From0_To8)
            .AddTeamWithAgent(3, " Team C", junior: 3, midLevel: 1, senior: 0, teamLead: 0,
                shift: AgentShifts.From16_To24)
            .Build();

        _agentPool.Initialize(agents, _agentBuilder.KickOverflowTeam);

        _sessionCoordinator.Run(stoppingToken);
        return Task.CompletedTask;
    }
}