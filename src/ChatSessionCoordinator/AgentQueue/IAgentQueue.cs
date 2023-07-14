using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.AgentQueue;

public interface IAgentQueue
{
    Agent Agent { get; set; }
    int ItemCount { get; }
    public Task<bool> QueueChat(ActorChat actorChat);
    public Task<ActorChat?> GetChatById(Guid chatId);
}