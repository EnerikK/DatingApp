using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Application.UserProfiles.Commands;

public class AddPhoto : IRequest<OperationResult<PhotoDto>>
{
    public Guid UserProfileId { get; set; }
    public List<PhotoDto>? Photos { get;  set; }
    public IFormFile File { get; set; }

}