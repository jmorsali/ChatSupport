using ChatSessionCoordinator.AgentPool;
using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Models.Entities;
using System.Collections.Concurrent;
using ChatSessionCoordinator.Extension;
using ChatSessionCoordinator.Models.Enums;
using Microsoft.Extensions.Options;

namespace ChatSessionCoordinator.SessionQueue;

public class InMemorySessionQueue : ISessionQueue
{
    private readonly SessionCoordinatorConfiguration _configuration;
    private readonly IAgentPool _agentPool;
    private readonly ConcurrentQueue<ActorChat> actorChatsMainQueue;
    public InMemorySessionQueue(IOptions<SessionCoordinatorConfiguration> configuration, IAgentPool agentPool)
    {
        _configuration = configuration.Value;
        _agentPool = agentPool;
        actorChatsMainQueue = new ConcurrentQueue<ActorChat>();
    }
    public async Task<bool> EnQueueChat(ActorChat actorChat)
    {
        await Task.Yield();
        if (actorChatsMainQueue.Count <= _configuration.MainQueueSize)
        {
            actorChatsMainQueue.Enqueue(actorChat);
            actorChat.Status =ChatStatus.PendingAssignment;
        }
        else if (DateTime.Now.IsWorkingHour() && _agentPool.HasOverflow)
        {
            actorChatsMainQueue.Enqueue(actorChat);
            actorChat.Status =ChatStatus.PendingAssignment;
        }
        else
        {
            actorChat.Status =ChatStatus.Refused;
            return false;
        }

        return true;
    }

    public async Task<ActorChat?> DequeueChat()
    {
        await Task.Yield();
        actorChatsMainQueue.TryDequeue(out var chat);
        return chat;
    }

    public async Task<ActorChat?> GetChatById(Guid chatId)
    {
        var chat = await _agentPool.GetChatById(chatId);
        if (chat != null)
        {
            chat.PollingCount++;
            return chat;
        }
        chat = actorChatsMainQueue.FirstOrDefault(c => c.ChatId == chatId);
        if (chat == null) return null;
        chat.PollingCount++;
        return chat;
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


}