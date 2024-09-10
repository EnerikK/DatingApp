using DatingApp.Api.Interfaces;
using DatingApp.Application.Enum;
using DatingApp.Application.Message.Queries;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Message.QueriesHandler;

public class GetMessageByIdHandler : IRequestHandler<GetMessageById,OperationResult<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessageByIdHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    
    public async Task<OperationResult<MessageDto>> Handle(GetMessageById request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<MessageDto>();

        var message = await _messageRepository.GetMessage(request.Id);
        if (message == null)
        {
            result.AddError(ErrorCode.UserNotFound,UserProfileErrorMessage.UserProfileNotFound);
            return result;
        }
        
        var messageDto = new MessageDto 
        {
            Id = message.Id,
            Content = message.Content,
            SenderUsername = message.Sender.UserProfileId.ToString(),
            RecipientUsername = message.Recipient.UserProfileId.ToString(),
            RecipientPhotoUrl = message.Recipient.BasicInfo.PhotoUrl,
            SenderPhotoUrl = message.Sender.BasicInfo.PhotoUrl,
        };
        result.PayLoad = messageDto;
        return result;
    }
}
