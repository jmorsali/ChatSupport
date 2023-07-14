namespace ChatSessionCoordinator.Models.DTOs;

public record ActorChatCreateDto
{
    public Guid ChatId { get; set; }
    public required string Title { get; set; }
    public required string MessageBody { get; set; }
    public List<byte[]> Attachments { get; set; }
}