using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Application.UserProfiles.Commands;

public class SetPhotoMainCommand : IRequest<OperationResult<PhotoDto>>
{
    public Guid UserProfileId { get; set; }
    public int photoId { get; set; }
}