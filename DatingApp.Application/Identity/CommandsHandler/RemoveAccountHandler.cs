using DatingApp.Application.Enum;
using DatingApp.Application.Identity.Commands;
using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Identity.CommandsHandler;

public class RemoveAccountHandler : IRequestHandler<RemoveAccount,OperationResult<bool>>
{
    private readonly DataContext _dataContext;
    
    public RemoveAccountHandler(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<OperationResult<bool>> Handle(RemoveAccount request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();

        try
        {
            var identityUser =
                await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == request.IdentityUserId.ToString(), cancellationToken);

            if (identityUser == null)
            {
                result.AddError(ErrorCode.IdentityUserDoesNotExist, ErrorMessages.NoExistingUser);
                return result;
            }

            var userProfile = await _dataContext.UserProfiles.FirstOrDefaultAsync(
                profile => profile.IdentityId == request.IdentityUserId.ToString(), cancellationToken);

            if (userProfile == null)
            {
                result.AddError(ErrorCode.NotFound, UserProfileErrorMessage.UserProfileNotFound);
                return result;
            }

            if (identityUser.Id != request.RequestedGuid.ToString())
            {
                result.AddError(ErrorCode.UnauthorizedAccountRemoval,ErrorMessages.UnauthorizedAccountRemoval);
                return result;
            }

            _dataContext.UserProfiles.Remove(userProfile);
            _dataContext.Users.Remove(identityUser);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}