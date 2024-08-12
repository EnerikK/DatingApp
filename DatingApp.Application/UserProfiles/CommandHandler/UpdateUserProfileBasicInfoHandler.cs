using DatingApp.Application.Enum;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Commands;
using DatingApp.DataAccess;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.UserProfiles.CommandHandler;


internal class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo,OperationResult<UserProfile>>
{
    private readonly DataContext _datactx;
    public UpdateUserProfileBasicInfoHandler(DataContext dataCtx)
    {
        _datactx = dataCtx;
    }
    public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserProfile>();
        try
        {
            var userProfile = await _datactx.UserProfiles
                .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.UserProfileId,cancellationToken: cancellationToken);

            if (userProfile is null)
            {
                result.AddError(ErrorCode.NotFound,string.Format(UserProfileErrorMessage.UserProfileNotFound,request.UserProfileId));
            }

            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName
                , request.EmailAddress, request.Phone, request.DateOfBirth, request.CurrentCity,
                request.KnownAs,request.Introduction,request.Interests,request.LookingFor,request.PhotoUrl);

            userProfile.UpdateBasicInfo(basicInfo);

            _datactx.UserProfiles.Update(userProfile);
            await _datactx.SaveChangesAsync(cancellationToken);

            result.PayLoad = userProfile;
            return result;
        }
        catch (Exception e)
        {
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}
