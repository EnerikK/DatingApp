using AutoMapper;
using DatingApp.Api.Filters;
using DatingApp.Application.Likes.Commands;
using DatingApp.Application.Likes.Queries;
using DatingApp.Application.UserProfiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class LikeController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public LikeController(IMediator mediator , IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;   
    }
    [HttpPost]
    [Route(ApiRoutes.UserLike.AddLike)]
    [ValidateGuid("id")]
    public async Task<IActionResult> AddLike(string sourceUserId, string targetUserId, CancellationToken cancellationToken)
    {
        var command = new AddLikeCommand
        {
            SourceUserId = Guid.Parse(sourceUserId),
            TargetUserId = Guid.Parse(targetUserId)
        };

        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsError) return HandleErrorResponse(response.Errors);
        return Ok(response.PayLoad);
    }

    [HttpGet]
    [Route(ApiRoutes.UserLike.LikedUsers)]
    [ValidateGuid("id")]
    public async Task<IActionResult> GetLikedUsers(string id, CancellationToken cancellationToken)
    {
        var query = new GetLikedUsers()
        {
            userProfileId = Guid.Parse(id)
        };
        
        var response = await _mediator.Send(query, cancellationToken);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        return Ok(response.PayLoad);
    }
    
    [HttpGet]
    [Route(ApiRoutes.UserLike.LikedUsersLikedBy)]
    [ValidateGuid("id")]
    public async Task<IActionResult> GetLikedUsersLikedBy(string id,CancellationToken cancellationToken)
    {
        var query = new GetLikedUsersLikedBy()
        {
            userProfileId = Guid.Parse(id)
        };

        var response = await _mediator.Send(query, cancellationToken);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        return Ok(response.PayLoad);
    }

    [HttpGet]
    [Route(ApiRoutes.UserLike.MutualLikes)]
    [ValidateGuid("id")]
    public async Task<IActionResult> GetMutualLikes(string id, CancellationToken cancellationToken)
    {
        var query = new GetMutualLikes()
        {
            userProfileId = Guid.Parse(id)
        };

        var response = await _mediator.Send(query, cancellationToken);

        if (response.IsError) return HandleErrorResponse(response.Errors);

        return Ok(response.PayLoad);
    }
    
}