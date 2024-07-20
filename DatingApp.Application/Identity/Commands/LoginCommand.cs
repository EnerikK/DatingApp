using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using MediatR;

namespace DatingApp.Application.Identity.Commands;

public class LoginCommand : IRequest<OperationResult<IdentityUserProfileDto>>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}