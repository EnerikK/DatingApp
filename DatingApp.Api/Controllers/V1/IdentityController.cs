using System.Security.Claims;
using AutoMapper;
using DatingApp.Api.Contracts.Identity;
using DatingApp.Api.Extensions;
using DatingApp.Api.Filters;
using DatingApp.Application.Identity.Commands;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IdentityController(IMediator mediator , IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;   
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Registration)]
        [ValidateModel]
        public async Task<IActionResult> Register(UserRegistration registration, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterIdentity>(registration);
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var map = _mapper.Map<IdentityUserProfile>(result.PayLoad);
            return Ok(map);
        }
        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult> Login(Login login, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginCommand>(login);
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var map = _mapper.Map<IdentityUserProfile>(result.PayLoad);
            return Ok(map);
        }
        [HttpDelete]
        [Route(ApiRoutes.Identity.IdentityById)]
        [ValidateGuid("identityUserId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAccount(string identityUserId, CancellationToken token)
        {
            var identityUserGuid = Guid.Parse(identityUserId);
            var requestedGuid = HttpContext.GetIdentityIdClaimValue();
            
            var command = new RemoveAccount
            {
                IdentityUserId = identityUserGuid,
                RequestedGuid = requestedGuid
            };

            var result = await _mediator.Send(command, token);
            if (result.IsError) HandleErrorResponse(result.Errors);
            
            return NoContent();
        }
        [HttpGet]
        [Route(ApiRoutes.Identity.CurrentUser)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CurrentUser(CancellationToken token)
        {
            var userProfileId = HttpContext.GetUserProfileClaimValue();

            var query = new GetCurrentUser
            {
                UserProfileId = userProfileId,
                ClaimsPrincipal = HttpContext.User
            };
            var result = await _mediator.Send(query,token);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            var map = _mapper.Map<IdentityUserProfile>(result.PayLoad);
            return Ok(map);
        }
    }
}
