using DatingApp.Application.Identity.Dtos;

namespace DatingApp.Api.Contracts.UserProfile.Requests;

public class UserProfileUploadPhoto
{
    public Guid UserProfileId { get; set; }
    public List<PhotoDto>? Photos { get;  set; }
    public IFormFile File { get; set; }

}