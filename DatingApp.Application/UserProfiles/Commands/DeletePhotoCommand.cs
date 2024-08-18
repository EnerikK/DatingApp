using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Application.UserProfiles.Commands;

public class DeletePhotoCommand : IRequest<OperationResult<PhotoDto>>
{
    public Guid UserProfileId { get; set; }
    public int photoId { get; set; }
    public IFormFile? File { get; set; }

}