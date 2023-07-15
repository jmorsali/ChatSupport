using ChatWindow.DTOs;

namespace ChatWindow;

internal interface IChatApiClient
{
    public Task<bool> CreateSupportRequest(Guid chatId, string title, string messageBody);
    public Task<ChatPollResponse> PollSupportRequest(Guid chatId);
}