using AutoMapper;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Identity.Queries;
using DatingApp.Application.Models;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Identity.QueriesHandler;

public class GetCurrentUserHandler : IRequestHandler<GetCurrentUser,OperationResult<IdentityUserProfileDto>>
{
    private readonly DataContext _dataContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMapper _mapper;
    private OperationResult<IdentityUserProfileDto> _result = new();
    
    public GetCurrentUserHandler(DataContext dataContext, UserManager<IdentityUser> userManager, IMapper mapper)
    {
        _dataContext = dataContext;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<OperationResult<IdentityUserProfileDto>> Handle(GetCurrentUser request, CancellationToken cancellationToken)
    {
        var identity = await _userManager.GetUserAsync(request.ClaimsPrincipal);
        var profile =
            await _dataContext.UserProfiles.FirstOrDefaultAsync(
                userProfile => userProfile.UserProfileId == request.UserProfileId, cancellationToken);

        _result.PayLoad = _mapper.Map<IdentityUserProfileDto>(profile);
        _result.PayLoad.UserName = identity.UserName;
        return _result;
    }
}