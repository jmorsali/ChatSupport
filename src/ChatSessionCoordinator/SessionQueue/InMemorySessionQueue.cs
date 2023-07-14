using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Models.DTOs;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Mappers;
using Microsoft.Extensions.Caching.Memory;

namespace ChatSessionCoordinator.SessionQueue;

public class InMemorySessionQueue : ISessionQueue
{
    private readonly Queue<ActorChat> actorChatsMainQueue;

    public InMemorySessionQueue(MemoryCache memoryCache, SessionCoordinatorConfiguration configuration)
    {
        actorChatsMainQueue = new Queue<ActorChat>(configuration.MainQueueSize);
        memoryCache.Set("ChatQueue", actorChatsMainQueue);

    }
    public async Task<bool> queueChat(ActorChatCreateDto actorChat)
    {
        actorChatsMainQueue.Enqueue(actorChat.Map());
        await Task.Yield();
        return true;
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