using DatingApp.Api.Interfaces;
using DatingApp.Application.Models;
using MediatR;

namespace DatingApp.Application.Message.Queries;

public class GetMessageById : IRequest<OperationResult<MessageDto>>
{
    public int Id { get; set; }
}