using ChatSessionCoordinator.Models.DTOs;
using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.SessionQueue;

public interface ISessionQueue
{
    public Task<bool> queueChat(ActorChatCreateDto actorChat);
    public Task<ActorChat> pollChat(Guid chatId);
    public Task<bool> resolveChat(Guid chatId);
    public Task<bool> refuseChat(Guid chatId);
    public Task<bool> inactiveChat(Guid chatId);
}