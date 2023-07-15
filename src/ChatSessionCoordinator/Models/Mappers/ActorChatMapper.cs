using ChatSessionCoordinator.Models.DTOs;
using ChatSessionCoordinator.Models.DTOs.Requests;
using ChatSessionCoordinator.Models.DTOs.Responses;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.Mappers;

public static class ActorChatMapper
{
    public static ActorChat Map(this ChatCreateRequest chatCreateRequest)
    {
        return new ActorChat
        {
            ChatId = chatCreateRequest.ChatId,
            MessageBody = chatCreateRequest.MessageBody,
            Title = chatCreateRequest.Title,
            Status = ChatStatus.New
        };
    }

    public static ChatCreateRequest MapToCreateDto(this ActorChat actorChat)
    {
        return new ChatCreateRequest
        {
            ChatId = actorChat.ChatId,
            MessageBody = actorChat.MessageBody,
            Title = actorChat.Title
        };
    }

    public static ChatPollResponse MapToPollResponse(this ActorChat actorChat)
    {
        return new ChatPollResponse
        {
            ChatId = actorChat.ChatId,
            MessageBody = actorChat.MessageBody,
            Title = actorChat.Title,
            Status = ChatStatus.New
        };
    }
}

