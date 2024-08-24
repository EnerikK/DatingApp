using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Dtos;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;

namespace DatingApp.Application.Likes.Commands;

public class AddLikeCommand : IRequest<OperationResult<UserProfileDto>>
{
    public Guid SourceUserId { get; set; }
    public Guid TargetUserId { get; set; }

}