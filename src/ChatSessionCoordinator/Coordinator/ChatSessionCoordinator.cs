﻿using ChatSessionCoordinator.AgentPool;
using ChatSessionCoordinator.Configurations;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;
using ChatSessionCoordinator.SessionQueue;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChatSessionCoordinator.Coordinator;

public class SessionCoordinator : ISessionCoordinator
{
    private readonly IAgentPool _agentPool;
    private readonly ILogger<ISessionCoordinator> _logger;
    private readonly SessionCoordinatorConfiguration _configuration;
    public ISessionQueue _sessionQueue { get; }

    public SessionCoordinator(ISessionQueue sessionQueue, IAgentPool agentPool, IOptions<SessionCoordinatorConfiguration> configuration, ILogger<ISessionCoordinator> logger)
    {
        _agentPool = agentPool;
        _logger = logger;
        _configuration = configuration.Value;
        _sessionQueue = sessionQueue;
    }
    public async void Run(CancellationToken cancellationToken)
    {
        while (true)
        {
            if (cancellationToken.IsCancellationRequested) return;

            var chat = await _sessionQueue.DequeueChat();
            if (chat == null)
            {
                await Task.Delay(1000, cancellationToken);
                continue;
            }

            var isQueuedSuccessfully = await ProcessChatQueue(chat);
            if (!isQueuedSuccessfully)
                await _sessionQueue.ReQueueChat(chat);
        }
    }

    public async Task<bool> ProcessChatQueue(ActorChat chat)
    {
        var agents = _agentPool.GetAvailableAgents();
        _logger.LogInformation($"There is {agents.Count} available");

        var agent = _agentPool.GetAvailableAgent();
        if (agent == null)
        {
            if (!_agentPool.HasOverflow)
            {
                _agentPool.KickOverflowTeam(_configuration.OverFlowCount);
                _logger.LogInformation($"There is no available agent.Kicking overflow team.");
                return false;
            }
        }

        if (agent is { IsOverflow: false })
            _agentPool.HasOverflow = false;

        if (agent != null)
        {
            await agent.Queue.QueueChat(chat);
            chat.Status = ChatStatus.Assigned;
            _logger.LogInformation($"new message is queued by agent {agent.AgentName}");
            return true;
        }
        else
        {
            _logger.LogCritical($"no more agent available. system is full");
            return false;
        }
    }
}