using DatingApp.Api.Interfaces;
using MediatR;

namespace DatingApp.Application.Message.Queries;

public class GetMessageThread : IRequest<IEnumerable<MessageDto>>
{
    public string CurrentUsername { get; set; }
    public string RecipientUsername { get; set; }
}
