using ChatSessionCoordinator.Models.DTOs;
using ChatSessionCoordinator.Models.DTOs.Requests;
using ChatSessionCoordinator.Models.Entities;
using ChatSessionCoordinator.Models.Enums;
using ChatWebApi.Controllers;

namespace ChatSessionCoordinator.Models.Mappers;

public static class ActorChatMapper
{
    public static ActorChat Map(this ActorChatCreateDto actorChatCreateDto)
    {
        return new ActorChat
        {
            Attachments = actorChatCreateDto.Attachments,
            ChatId = actorChatCreateDto.ChatId,
            MessageBody = actorChatCreateDto.MessageBody,
            Title = actorChatCreateDto.Title,
            Status = ChatStatus.New
        };
    }

    public static ActorChatCreateDto  MapToCreateDto(this ActorChat  actorChatCreateDto)
    {
        throw new NotImplementedException();
    }

    public static ChatPollResponse MapToPollResponse(this ActorChat actorChatCreateDto)
    {
        throw new NotImplementedException();
    }


    public static ActorChatCreateDto Map(this ChatCreateRequest chatCreateRequest)
    {
        throw new NotImplementedException();
    }
}

