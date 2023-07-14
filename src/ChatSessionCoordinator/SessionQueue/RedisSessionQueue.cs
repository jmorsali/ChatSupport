using ChatSessionCoordinator.Models.DTOs;
using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.SessionQueue;

public class RedisSessionQueue : ISessionQueue
{
    public Task<bool> queueChat(ActorChatCreateDto actorChat)
    {
        throw new NotImplementedException();
    }

    public Task<ActorChat> pollChat(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> resolveChat(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> refuseChat(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> inactiveChat(Guid chatId)
    {
        throw new NotImplementedException();
    }
}