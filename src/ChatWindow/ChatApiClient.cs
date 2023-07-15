using ChatWindow.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace ChatWindow;

internal class ChatApiClient : IChatApiClient
{
    private readonly HttpClient _httpClient;

    public ChatApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateSupportRequest(Guid chatId, string title, string messageBody)
    {
        dynamic chatCreateRequest = new { chatId, title, messageBody };
        var jsonContent = JsonConvert.SerializeObject(chatCreateRequest);
        HttpContent httpContent = new StringContent(jsonContent);

        var response = await _httpClient.PutAsync("/api/v1/chat/create", httpContent);
        if (response.IsSuccessStatusCode)
        {
            var result=await response.Content.ReadAsStringAsync();
            dynamic res= JsonConvert.DeserializeObject(result);
            return (bool)res?.Result;
        }

        return false;
    }

    public async Task<ChatPollResponse> PollSupportRequest(Guid chatId)
    {
        var response = await _httpClient.GetFromJsonAsync<ChatPollResponse>($"/api/v1/chat/poll/{chatId}");
        return response;
    }
}