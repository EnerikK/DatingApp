using AutoMapper;
using DatingApp.Api.Contracts.Identity;
using DatingApp.Api.Interfaces;
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
    private readonly UsersController _usersController;

    public MessageController(IMediator mediator , IMapper mapper,IMessageRepository messageRepository,UsersController usersController)
    {
        _mediator = mediator;
        _mapper = mapper;
        _messageRepository = messageRepository;
        _usersController = usersController;
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto,IdentityUserProfile identityUserProfile,CancellationToken cancellationToken)
    {
        var username = identityUserProfile.UserProfileId.ToString();
        if (username == createMessageDto.RecipientUsername.ToLower()) return BadRequest("You cant message yourself");

        var sender = await _usersController.GetUserProfileById(username, cancellationToken);
        var recipient = await _usersController.GetUserProfileById(createMessageDto.RecipientUsername, cancellationToken);
        if (recipient == null || sender == null) return BadRequest("Cannot send message at this time");
        
        var message = new Message()
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = 
        }
    }
    
}