using ChatSessionCoordinator.Models.Entities;
using System.Collections.Concurrent;

namespace ChatSessionCoordinator.AgentQueue;

public class InMemoryAgentQueue : IAgentQueue
{
    private ConcurrentQueue<ActorChat> agentQueue { get; set; }
    public InMemoryAgentQueue()
    {
        agentQueue = new ConcurrentQueue<ActorChat>();
    }

    public Agent Agent { get; set; }
    public int ItemCount => agentQueue.Count;

    public async Task<bool> QueueChat(ActorChat actorChat)
    {

        agentQueue.Enqueue(actorChat);
        await Task.Yield();
        return true;
    }

    public async Task<ActorChat?> GetChatById(Guid chatId)
    {
        await Task.Yield();
        return agentQueue.FirstOrDefault(c => c.ChatId == chatId);
    }
}