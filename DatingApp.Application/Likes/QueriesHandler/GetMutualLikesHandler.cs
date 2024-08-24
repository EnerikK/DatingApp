using DatingApp.Application.Enum;
using DatingApp.Application.Likes.Queries;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Dtos;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Likes.QueriesHandler;

public class GetMutualLikesHandler : IRequestHandler<GetMutualLikes,OperationResult<List<UserProfileDto>>>
{
    private readonly DataContext _dataContext;

    public GetMutualLikesHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<OperationResult<List<UserProfileDto>>> Handle(GetMutualLikes request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<UserProfileDto>>();
        try
        {
            
            var userProfile = await _dataContext.UserProfiles
                .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.userProfileId, cancellationToken);

            if (userProfile == null)
            {
                result.AddError(ErrorCode.UserNotFound, $"User with ID {request.userProfileId} not found.");
                return result;
            }

            var mutualLikes = await (from like in _dataContext.Likes
                    join otherLike in _dataContext.Likes
                        on like.TargetUserId equals otherLike.SourceUserId
                    where like.SourceUserId == request.userProfileId
                          && otherLike.TargetUserId == request.userProfileId
                    select otherLike.SourceUser)
                .Distinct()
                .ToListAsync(cancellationToken);

            if (mutualLikes != null && mutualLikes.Any())
            {
                result.PayLoad = mutualLikes.Select(UserProfileDto.FromUserProfile).ToList(); 
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