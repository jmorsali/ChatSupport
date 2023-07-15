namespace ChatWindow.DTOs;

public class ChatPollResponse
{
    public Guid ChatId { get; set; }
    public required string Title { get; set; }
    public required string MessageBody { get; set; }
    public ChatStatus Status { get; set; }
}