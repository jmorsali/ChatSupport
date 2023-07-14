using ChatSessionCoordinator.AgentPool;
using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.SessionQueue;

namespace ChatSessionCoordinator.Coordinator;

public class ChatSessionCoordinator : IChatSessionCoordinator
{
    private readonly IAgentPool _agentPool;
    private readonly SessionCoordinatorConfiguration _configuration;
    private ISessionQueue _sessionQueue { get; }

    public ChatSessionCoordinator(ISessionQueue sessionQueue, IAgentPool agentPool,SessionCoordinatorConfiguration configuration)
    {
        _agentPool = agentPool;
        _configuration = configuration;
        _sessionQueue = sessionQueue;
    }
    public async void Run(CancellationToken cancellationToken)
    {
        while (true)
        {
            if (cancellationToken.IsCancellationRequested) return;

            var chat = await _sessionQueue.DequeueChat();
            if (chat == null)
            {
                await Task.Delay(1000, cancellationToken);
                continue;
            }

            var agent = _agentPool.GetAvailableAgent();
            if (agent == null)
            {
                _agentPool.KickOverflowTeam(_configuration.OverFlowCount);
                continue;
            }

            agent.Queue.QueueChat(chat);

        }
    }
}