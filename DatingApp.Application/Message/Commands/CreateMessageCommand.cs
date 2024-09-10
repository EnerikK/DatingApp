using DatingApp.Api.Interfaces;
using MediatR;

namespace DatingApp.Application.Message.Commands;

public class CreateMessageCommand : IRequest<MessageDto>
{
    public string SenderUsername { get; set; }
    public string RecipientUsername { get; set; }
    public string Content { get; set; }
}