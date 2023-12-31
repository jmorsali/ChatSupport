using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.DTOs.Responses;

public class ChatPollResponse
{
    public Guid ChatId { get; set; }
    public required string Title { get; set; }
    public required string MessageBody { get; set; }
    public ChatStatus Status { get; set; }
}