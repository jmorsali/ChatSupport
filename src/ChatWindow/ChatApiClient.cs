using ChatWindow.DTOs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ChatWindow;

internal class ChatApiClient : IChatApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ChatApiClient> _logger;

    public ChatApiClient(HttpClient httpClient, ILogger<ChatApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> CreateSupportRequest(Guid chatId, string title, string messageBody)
    {
        try
        {
            var chatCreateRequest = new ChatCreateRequest { ChatId = chatId, Title = title, MessageBody = messageBody };
            var response = await _httpClient.PutAsJsonAsync("/api/v1/chat/create", chatCreateRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ChatCreateResponse>(result);
                return res.Result;
            }

            _logger.LogError($"Invalid api Response: {response.StatusCode}--{response.ReasonPhrase}");
            return false;
        }
        catch(Exception ex) 
        {
            _logger.LogCritical($"exception api call: {ex}");
            return false;
        }
    }

    public async Task<ChatPollResponse> PollSupportRequest(Guid chatId)
    {
        var response = await _httpClient.GetFromJsonAsync<ChatPollResponse>($"/api/v1/chat/poll/{chatId}");
        return response;
    }
}