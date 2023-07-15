using ChatSessionCoordinator.Models.DTOs;
using ChatSessionCoordinator.Models.DTOs.Requests;
using ChatSessionCoordinator.Models.DTOs.Responses;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;

namespace ChatSessionCoordinator.Models.Mappers;

public static class ActorChatMapper
{
    public static ActorChat Map(this ActorChatCreateDto actorChatCreateDto)
    {
        return new ActorChat
        {
            ChatId = actorChatCreateDto.ChatId,
            MessageBody = actorChatCreateDto.MessageBody,
            Title = actorChatCreateDto.Title,
            Status = ChatStatus.New
        };
    }

    public static ActorChatCreateDto MapToCreateDto(this ActorChat actorChat)
    {
        return new ActorChatCreateDto
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


    public static ActorChatCreateDto Map(this ChatCreateRequest chatCreateRequest)
    {
        throw new NotImplementedException();
    }
}

