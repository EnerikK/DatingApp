using System.Security.Claims;
using AutoMapper;
using DatingApp.Application.Enum;
using DatingApp.Application.Identity.Commands;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using DatingApp.Application.Services;
using DatingApp.DataAccess;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DatingApp.Application.Identity.CommandsHandler;

public class LoginCommandHandler : IRequestHandler<LoginCommand,OperationResult<IdentityUserProfileDto>>
{
    private readonly DataContext _dataContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IdentityService _identityService;
    private readonly IMapper _mapper;
    private OperationResult<IdentityUserProfileDto> _result = new();

    public LoginCommandHandler(DataContext dataContext , UserManager<IdentityUser> userManager,IdentityService identityService,IMapper mapper)
    {
        _dataContext = dataContext;
        _userManager = userManager;
        _identityService = identityService;
        _mapper = mapper;
    }
    public async Task<OperationResult<IdentityUserProfileDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var identityUser = await ValidateAndGetIdentity(request);
            if (_result.IsError) return _result;
            
            var userProfile =
                await _dataContext.UserProfiles.FirstOrDefaultAsync(userP => userP.IdentityId == identityUser.Id,cancellationToken);

            _result.PayLoad = _mapper.Map<IdentityUserProfileDto>(userProfile);
            _result.PayLoad.UserName = identityUser.UserName;
            _result.PayLoad.Token = GetJWTString(identityUser, userProfile);
            return _result;
        }
        catch (Exception e)
        {
            _result.AddUnknownError(e.Message);
        }

        return _result;
    }
    private async Task<IdentityUser> ValidateAndGetIdentity(LoginCommand request)
    {
        
        var identityUser = await _userManager.FindByEmailAsync(request.Username);
        if (identityUser is null) _result.AddError(ErrorCode.IdentityUserAlreadyExists,ErrorMessages.NoExistingUser);

        var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);
        if(!validPassword) _result.AddError(ErrorCode.IdentityUserDoesNotExist,ErrorMessages.IncorrectPassword);

        return identityUser;
    }
    private string GetJWTString(IdentityUser identityUser, UserProfile userProfile)
    {
        var claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
            new Claim("IdentityId", identityUser.Id),
            new Claim("UserProfileId", userProfile.UserProfileId.ToString())
        });
     
        var token = _identityService.CreateSecurityToken(claimsIdentity);
        return _identityService.WriteToken(token);
    }
}