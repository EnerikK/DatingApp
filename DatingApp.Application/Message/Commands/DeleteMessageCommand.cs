using DatingApp.Api.Interfaces;
using MediatR;

namespace DatingApp.Application.Message.Commands;

public class DeleteMessageCommand : IRequest<bool>
{
    public int MessageId { get; set; }
}