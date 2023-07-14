using ChatSessionCoordinator.Models.DTOs;
using ChatSessionCoordinator.Models.Entities;

namespace ChatSessionCoordinator.SessionQueue;

public class RedisSessionQueue : ISessionQueue
{
    public Task<bool> EnQueueChat(ActorChatCreateDto actorChat)
    {
        throw new NotImplementedException();
    }

    public Task<ActorChat> GetChatById(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ResolveChat(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RefuseChat(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InactiveChat(Guid chatId)
    {
        throw new NotImplementedException();
    }

    public Task<ActorChat?> DequeueChat()
    {
        throw new NotImplementedException();
    }
}