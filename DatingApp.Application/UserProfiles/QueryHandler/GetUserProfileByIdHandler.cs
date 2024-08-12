using DatingApp.Application.Enum;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Dtos;
using DatingApp.Application.UserProfiles.Queries;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.UserProfiles.QueryHandler;

public class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById,OperationResult<UserProfileDto>>
{
    private readonly DataContext _dataContext;

    public GetUserProfileByIdHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<OperationResult<UserProfileDto>> Handle(GetUserProfileById request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserProfileDto>();
        var profile  = await _dataContext.UserProfiles.Include(x => x.Photos).FirstOrDefaultAsync(
            userProfile => userProfile.UserProfileId == request.UserProfileId,cancellationToken);
            
        if (profile is null) //Checking if the userprofile with this specific id exists
        {
            result.AddError(ErrorCode.NotFound,string.Format(UserProfileErrorMessage.UserProfileNotFound,request.UserProfileId));
        }
        
        result.PayLoad = UserProfileDto.FromUserProfile(profile);
        return result;
    }
}