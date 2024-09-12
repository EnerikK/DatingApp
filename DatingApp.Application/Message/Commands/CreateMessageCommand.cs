using DatingApp.Api.Interfaces;
using DatingApp.Application.Models;
using MediatR;

namespace DatingApp.Application.Message.Commands;

public class CreateMessageCommand : IRequest<OperationResult<MessageDto>>
{
    public Guid SenderUsername { get; set; }
    public Guid RecipientUsername { get; set; }
    public string Content { get; set; }
}