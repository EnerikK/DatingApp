using DatingApp.Application.Enum;
using DatingApp.Application.Likes.Queries;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles;
using DatingApp.Application.UserProfiles.Dtos;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Likes.QueriesHandler;

public class GetLikedUsersHandler : IRequestHandler<GetLikedUsers,OperationResult<List<UserProfileDto>>>
{
    private readonly DataContext _dataContext;

    public GetLikedUsersHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<OperationResult<List<UserProfileDto>>> Handle(GetLikedUsers request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<UserProfileDto>>();

        try
        {
            var userProfile = await _dataContext.UserProfiles
                .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.userProfileId,cancellationToken);

            if (userProfile == null)
            {
                result.AddError(ErrorCode.UserNotFound,UserProfileErrorMessage.UserProfileNotFound);
                return result;
            }

            var likedUsers = await _dataContext.Likes
                .Where(x => x.SourceUserId == request.userProfileId)
                .Select(x => x.TargetUser)
                .ToListAsync(cancellationToken);

            if (likedUsers != null && likedUsers.Any())
            {
                result.PayLoad = likedUsers.Select(UserProfileDto.FromUserProfile).ToList();
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