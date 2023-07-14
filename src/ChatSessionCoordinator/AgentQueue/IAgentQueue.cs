using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.AgentQueue;

public interface IAgentQueue
{
    Agent Agent { get; set; }
    public Task<bool> QueueChat(ActorChat actorChat);
    public Task<ActorChat?> GetChatById(Guid chatId);
}