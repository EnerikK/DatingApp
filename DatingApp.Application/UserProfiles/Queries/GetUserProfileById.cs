using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Dtos;
using MediatR;

namespace DatingApp.Application.UserProfiles.Queries;

public class GetUserProfileById : IRequest<OperationResult<UserProfileDto>>
{
    public Guid UserProfileId { get; set; }
}