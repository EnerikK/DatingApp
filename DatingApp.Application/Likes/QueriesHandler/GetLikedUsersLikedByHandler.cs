using DatingApp.Application.Enum;
using DatingApp.Application.Identity.Queries;
using DatingApp.Application.Likes.Queries;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Dtos;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Likes.QueriesHandler;

public class GetLikedUsersLikedByHandler : IRequestHandler<GetLikedUsersLikedBy,OperationResult<List<UserProfileDto>>>
{
    private readonly DataContext _dataContext;

    public GetLikedUsersLikedByHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<OperationResult<List<UserProfileDto>>> Handle(GetLikedUsersLikedBy request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<UserProfileDto>>();
        try
        {
            var userProfile = await _dataContext.UserProfiles
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.userProfileId,cancellationToken);

            if (userProfile == null)
            {
                result.AddError(ErrorCode.UserNotFound, $"User with ID {request.userProfileId} not found.");
                return result;
            }
            
            var likedByUsers  = await _dataContext.Likes
                .Where(x => x.TargetUserId == request.userProfileId)
                .Select(x => x.SourceUser)
                .ToListAsync(cancellationToken);

            if (likedByUsers != null && likedByUsers.Any())
            {
                result.PayLoad = likedByUsers.Select(UserProfileDto.FromUserProfile).ToList(); // Convert to DTOs
            }
            else
            {
                result.PayLoad = new List<UserProfileDto>();
            }

            return result;
        }
        catch (Exception e)
        {
            result.AddUnknownError(e.Message);

        }
        return result;
    }
}