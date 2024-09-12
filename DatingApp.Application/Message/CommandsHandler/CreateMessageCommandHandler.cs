using DatingApp.Api.Interfaces;
using DatingApp.Application.Enum;
using DatingApp.Application.Message.Commands;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Message.CommandsHandler;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand,OperationResult<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly DataContext _dataContext; 

    public CreateMessageCommandHandler(IMessageRepository messageRepository, DataContext dataContext)
    {
        _messageRepository = messageRepository;
        _dataContext = dataContext;
    }
    
    public async Task<OperationResult<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<MessageDto>();
        
        var sender = await _dataContext.UserProfiles
            .FirstOrDefaultAsync(up => up.UserProfileId == request.SenderUsername, cancellationToken);

        var recipient = await _dataContext.UserProfiles
            .FirstOrDefaultAsync(up => up.UserProfileId == request.RecipientUsername, cancellationToken);

        if (recipient == null || sender == null)
        {
            result.AddError(ErrorCode.MessageNotFound,MessageErrorMessage.UserMessageNotFound);
            return result;
        }

        var message = new Domain.Aggregates.UserProfileAggregates.Message()
        {
            Sender = sender,
            Recipient = recipient,
            SenderId = sender.UserProfileId, 
            RecipientId = recipient.UserProfileId, 
            SenderUsername = sender.IdentityId, 
            RecipientUsername = recipient.IdentityId, 
            Content = request.Content,
            MessageSent = DateTime.UtcNow
        };

        _messageRepository.AddMessage(message);

        var messageDto = new MessageDto 
        {
            Id = message.Id,
            SenderGuid = message.SenderId,
            RecipientGuid = message.RecipientId,
            SenderUsername = message.SenderUsername,
            RecipientUsername = message.RecipientUsername,
            RecipientPhotoUrl = message.Recipient.BasicInfo.PhotoUrl,
            SenderPhotoUrl = message.Sender.BasicInfo.PhotoUrl,
            Content = message.Content,
            DateRead = message.MessageSent
        };
        
        await _messageRepository.SaveAllAsync();

        result.PayLoad = messageDto;
        return result;
    }
}