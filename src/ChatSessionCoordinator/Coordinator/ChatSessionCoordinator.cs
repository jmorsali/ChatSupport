using ChatSessionCoordinator.AgentPool;
using ChatSessionCoordinator.SessionQueue;

namespace ChatSessionCoordinator.Coordinator;

public class ChatSessionCoordinator : IChatSessionCoordinator
{
    private readonly IAgentPool _agentPool;
    private ISessionQueue _sessionQueue { get; }

    public ChatSessionCoordinator(ISessionQueue sessionQueue, IAgentPool agentPool)
    {
        _agentPool = agentPool;
        _sessionQueue = sessionQueue;
    }
    public async void Run(CancellationToken cancellationToken)
    {
        while (true)
        {
            if (cancellationToken.IsCancellationRequested) return;

            var chat = await _sessionQueue.DequeueChat();
            if (chat == null) await Task.Delay(1000, cancellationToken);

            var agent = _agentPool.GetAvailableAgent();

        }
    }
}