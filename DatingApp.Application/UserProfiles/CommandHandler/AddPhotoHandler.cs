using DatingApp.Application.Enum;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Commands;
using DatingApp.DataAccess;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.UserProfiles.CommandHandler;

public class AddPhotoHandler : IRequestHandler<UpdateUserProfileBasicInfo,OperationResult<UserProfile>>
{
    private readonly DataContext _datactx;
    
    public AddPhotoHandler(DataContext dataCtx)
    {
        _datactx = dataCtx;
    }
    public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserProfile>();

        var userProfile = await _datactx.UserProfiles
            .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.UserProfileId,cancellationToken: cancellationToken);

        if (userProfile is null)
        {
            result.AddError(ErrorCode.NotFound,string.Format(UserProfileErrorMessage.UserProfileNotFound,request.UserProfileId));
        }

        return result;


    }
}