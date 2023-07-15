using ChatSessionCoordinator.Models.DTOs;
using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.SessionQueue;

public interface ISessionQueue
{
    public Task<bool> EnQueueChat(ActorChat actorChat);
    public Task<ActorChat?> GetChatById(Guid chatId);
    public Task<bool> ResolveChat(Guid chatId);
    public Task<bool> RefuseChat(Guid chatId);
    public Task<bool> InactiveChat(Guid chatId);
    public Task<ActorChat?> DequeueChat();
}