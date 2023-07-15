using ChatSessionCoordinator.Models.Entities;
using System.Collections.Concurrent;
using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.AgentQueue;

public class InMemoryAgentQueue : IAgentQueue
{
    private readonly ConcurrentQueue<ActorChat> agentQueue;
    public InMemoryAgentQueue()
    {
        agentQueue = new ConcurrentQueue<ActorChat>();
    }

    public Agent Agent { get; set; }
    public int ItemCount => agentQueue.Count;

    public async Task<bool> QueueChat(ActorChat actorChat)
    {
        agentQueue.Enqueue(actorChat);
        Agent.LastAssignment=DateTime.Now;
        if (agentQueue.Count >= Agent.Team?.MaximumQueueLenght)
            Agent.Statuses = AgentStatuses.Busy;
        await Task.Yield();
        return true;
    }

    public async Task<ActorChat?> GetChatById(Guid chatId)
    {
        await Task.Yield();
        return agentQueue.FirstOrDefault(c => c.ChatId == chatId);
    }
}