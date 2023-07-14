using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.Entities;

public class ActorChat
{
    public Guid ChatId { get; set; }
    public string Title { get; set; }
    public string MessageBody { get; set; }
    public List<byte[]> Attachments { get; set; }
    public ChatStatus Status { get; set; }
}