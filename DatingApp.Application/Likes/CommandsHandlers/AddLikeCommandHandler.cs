using DatingApp.Application.Enum;
using DatingApp.Application.Likes.Commands;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles;
using DatingApp.Application.UserProfiles.Dtos;
using DatingApp.DataAccess;
using DatingApp.DataAccess.Migrations;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Likes.CommandsHandlers;

public class AddLikeCommandHandler : IRequestHandler<AddLikeCommand,OperationResult<UserProfileDto>>
{
    private readonly DataContext _dataContext;

    public AddLikeCommandHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<OperationResult<UserProfileDto>> Handle(AddLikeCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserProfileDto>();

        var userProfile = await _dataContext.UserProfiles
            .Include(u => u.LikedUsers)
            .ThenInclude(l => l.TargetUser)
            .ThenInclude(p => p.Photos)
            .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.SourceUserId, cancellationToken);

        if (userProfile == null)
        {
            result.AddError(ErrorCode.UserNotFound, $"User with ID {request.SourceUserId} not found.");
            return result;
        }
        
        var targetUserProfile = await _dataContext.UserProfiles
            .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.TargetUserId, cancellationToken);


        if (request.SourceUserId == request.TargetUserId)
        {
            result.AddError(ErrorCode.TargetIdAndSourceIdAreTheSame, string.Format(UserProfileErrorMessage.IdsAreTheSame, request.SourceUserId));
            return result;
        }

        var existingLike = userProfile.LikedUsers.FirstOrDefault(l => l.TargetUserId == request.TargetUserId);

        if (existingLike == null)
        {
            var like = new UserLike
            {
                SourceUserId = request.SourceUserId,
                SourceUser = userProfile,
                TargetUserId = request.TargetUserId,
                TargetUser = targetUserProfile
            };

            userProfile.AddLike(like);
        }
        else
        {
            userProfile.DeleteLike(request.TargetUserId);
        }

        await _dataContext.SaveChangesAsync(cancellationToken);

        result.PayLoad = UserProfileDto.FromUserProfile(userProfile);
        return result;

    }
  
}