namespace ChatSessionCoordinator.Models.DTOs.Requests
{
    public class ChatCreateRequest
    {
        public Guid ChatId { get; set; }
        public required string Title { get; set; }
        public required string MessageBody { get; set; }
    }
}