﻿using AutoMapper;
using DatingApp.Api.Contracts.UserProfile.Responses;
using DatingApp.Application.UserProfiles.Queries;
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

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles(CancellationToken cancellationToken)
        {
            var query = new GetAllUserProfiles();
            var response = await _mediator.Send(query, cancellationToken);
            var profiles = _mapper.Map<List<UserProfileResponse>>(response.PayLoad);
            return Ok(profiles);
        }
    }
}