using DatingApp.Application.Models;
using MediatR;

namespace DatingApp.Application.Identity.Commands;

public class RemoveAccount : IRequest<OperationResult<bool>>
{
    public Guid IdentityUserId { get; set; }
    public Guid RequestedGuid { get; set; }
}