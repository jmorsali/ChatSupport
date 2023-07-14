namespace ChatSessionCoordinator.Coordinator;

public class ChatSessionCoordinator :IChatSessionCoordinator
{
    public void Run(CancellationToken cancellationToken)
    {
        while (true)
        {
            if(cancellationToken.IsCancellationRequested) return;

        }
    }
}