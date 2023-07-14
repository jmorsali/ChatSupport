using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.AgentQueue;

public class InMemoryAgentQueue : IAgentQueue
{
    private Queue<ActorChat> agentQueue { get; set; }
    public InMemoryAgentQueue(Agent agent)
    {
        Agent = agent;
        agentQueue = new Queue<ActorChat>();
    }

    public Agent Agent { get; set; }
    public async Task<bool> QueueChat(ActorChat actorChat)
    {
        agentQueue.Enqueue(actorChat);
        await Task.Yield();
        return true;
    }

}