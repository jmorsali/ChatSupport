using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.AgentQueue;

public class RedisAgentQueue : IAgentQueue
{
    public Agent Agent { get; set; }
    public Task<bool> QueueChat(ActorChat actorChat)
    {
        throw new NotImplementedException();
    }

    public Task<ActorChat?> GetChatById(Guid chatId)
    {
        throw new NotImplementedException();
    }
}