using System.Security.Claims;
using AutoMapper;
using DatingApp.Api.Contracts.Identity;
using DatingApp.Api.Extensions;
using DatingApp.Api.Interfaces;
using DatingApp.Application.Message.Commands;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers.V1;


[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class MessageController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;

    public MessageController(IMediator mediator , IMapper mapper,IMessageRepository messageRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _messageRepository = messageRepository;
    }

    [HttpPost]
    [Route(ApiRoutes.Message.CreateMessage)]
    public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto,CancellationToken cancellationToken)
    {
        /*foreach (var claim in User.Claims)
        {
            Console.WriteLine($"ClaimType: {claim.Type}, Value: {claim.Value}");
        }*/
        var identityUserId = User.FindFirst("UserProfileId")?.Value;
        if (string.IsNullOrEmpty(identityUserId) || !Guid.TryParse(identityUserId, out var identityUserGuid))
        {
            return Unauthorized("User not authenticated or invalid user ID format.");
        }
        var command = new CreateMessageCommand()
        {
            SenderUsername = identityUserGuid,
            RecipientUsername = createMessageDto.RecipientUsername,
            Content = createMessageDto.Content
        };

        var response = await _mediator.Send(command, cancellationToken);
        if (response.IsError) return HandleErrorResponse(response.Errors);
        return Ok(response.PayLoad);
    }
    
}