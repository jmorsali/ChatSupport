namespace ChatWindow.DTOs;

public record ChatCreateRequest
{
    public Guid ChatId { get; set; }
    public required string Title { get; set; }
    public required string MessageBody { get; set; }
}