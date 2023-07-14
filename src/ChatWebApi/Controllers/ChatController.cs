using ChatSessionCoordinator.Models.DTOs.Requests;
using ChatSessionCoordinator.Models.DTOs.Responses;
using ChatSessionCoordinator.Models.Mappers;
using ChatSessionCoordinator.SessionQueue;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/Chat")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly ISessionQueue _sessionQueue;

        public ChatController(ILogger<ChatController> logger, ISessionQueue sessionQueue)
        {
            _logger = logger;
            _sessionQueue = sessionQueue;
        }

        [HttpPut("Create")]
        public async Task<ActionResult<ChatCreateResponse>> CreateChatSession([FromBody] ChatCreateRequest chatCreateRequest)
        {
            await _sessionQueue.EnQueueChat(chatCreateRequest.Map());
            return Ok(new ChatCreateResponse());
        }

        [HttpGet("Poll")]
        public async Task<ActionResult<ChatPollResponse>> PollChatSession(Guid chatId)
        {
            var chat = await _sessionQueue.GetChatById(chatId);
            return Ok(chat?.MapToPollResponse());
        }
    }
}