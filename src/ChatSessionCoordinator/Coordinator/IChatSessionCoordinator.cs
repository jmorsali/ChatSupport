namespace ChatSessionCoordinator.Coordinator;

public interface IChatSessionCoordinator
{
    public void Run(CancellationToken cancellationToken) ;
    
}