using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.SessionQueue;

namespace ChatSessionCoordinator.Coordinator;

public interface ISessionCoordinator
{
    public ISessionQueue _sessionQueue { get; }
    public void Run(CancellationToken cancellationToken) ;
    public Task<bool> ProcessChatQueue(ActorChat chat);

}