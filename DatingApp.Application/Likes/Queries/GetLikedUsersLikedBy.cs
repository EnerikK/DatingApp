using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Dtos;
using MediatR;

namespace DatingApp.Application.Likes.Queries;

public class GetLikedUsersLikedBy : IRequest<OperationResult<List<UserProfileDto>>>
{
    public Guid userProfileId { get; set; }

}