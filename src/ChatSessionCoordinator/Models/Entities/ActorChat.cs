using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.Entities;

public class ActorChat
{
    public Guid ChatId { get; set; }
    public required string Title { get; set; }
    public required string MessageBody { get; set; }
    public List<byte[]>? Attachments { get; set; }
    public ChatStatus Status { get; set; }
    public Agent? AssignedAgent { get; set; }
    public int PollingCount { get; set; } = 0;
}