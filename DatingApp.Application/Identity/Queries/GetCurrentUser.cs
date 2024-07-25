using System.Security.Claims;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using MediatR;

namespace DatingApp.Application.Identity.Queries;

public class GetCurrentUser : IRequest<OperationResult<IdentityUserProfileDto>>
{
    public Guid UserProfileId { get; set; }
    public ClaimsPrincipal ClaimsPrincipal { get; set; }
}