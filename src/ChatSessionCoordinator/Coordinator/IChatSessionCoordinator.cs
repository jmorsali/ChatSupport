namespace ChatSessionCoordinator.Coordinator;

public interface ISessionCoordinator
{
    public void Run(CancellationToken cancellationToken) ;
    
}