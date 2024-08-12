using AutoMapper;
using DatingApp.Api.Contracts.UserProfile.Requests;
using DatingApp.Api.Contracts.UserProfile.Responses;
using DatingApp.Api.Filters;
using DatingApp.Api.Services;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.UserProfiles.Commands;
using DatingApp.Application.UserProfiles.Queries;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route(ApiRoutes.BaseRoute)]
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IMediator mediator, IMapper mapper,IPhotoService photoService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _photoService = photoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles(CancellationToken cancellationToken)
        {
            var query = new GetAllUserProfiles();
            var response = await _mediator.Send(query, cancellationToken);
            var profiles = _mapper.Map<List<UserProfileResponse>>(response.PayLoad);
            return Ok(profiles);
        }
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [HttpGet]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetUserProfileById(string id, CancellationToken cancellationToken)
        {
            var query = new GetUserProfileById
            {
                UserProfileId = Guid.Parse(id)
            };
            
            var response = await _mediator.Send(query, cancellationToken);

            if (response.IsError) return HandleErrorResponse(response.Errors);

            var userProfile = UserProfileResponse.UserProfileDto(response.PayLoad);

            return Ok(userProfile);
        }
        [HttpPatch]
        [Route(ApiRoutes.UserProfiles.IdRoute)]
        [ValidateModel]
        [ValidateGuid("id")]
        public async Task<IActionResult> UpdateUserProfile(string id, UserProfileCreateUpdate updateProfile,CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateUserProfileBasicInfo>(updateProfile);
            command.UserProfileId = Guid.Parse(id);
            var response = await _mediator.Send(command,cancellationToken);

            if (response.IsError)  HandleErrorResponse(response.Errors);
            return NoContent();
        }

        [HttpPost]
        [Route(ApiRoutes.UserProfiles.AddPhoto)]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            return NoContent();
        }
    }
}
