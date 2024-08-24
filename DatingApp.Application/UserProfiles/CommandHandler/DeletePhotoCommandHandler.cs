using DatingApp.Application.Enum;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using DatingApp.Application.Services;
using DatingApp.Application.UserProfiles.Commands;
using DatingApp.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.UserProfiles.CommandHandler;

public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand,OperationResult<PhotoDto>>
{
    private readonly DataContext _dataContext;
    private readonly IPhotoService _photoService;
    public DeletePhotoCommandHandler(DataContext dataContext,IPhotoService photoService)
    {
        _dataContext = dataContext;
        _photoService = photoService;
    }
    public async Task<OperationResult<PhotoDto>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
       var result =  new OperationResult<PhotoDto>();
       
       
       var userProfile = await _dataContext.UserProfiles.Include(x => x.Photos)
           .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.UserProfileId,cancellationToken: cancellationToken);
        
       if (userProfile is null)
       {
           result.AddError(ErrorCode.NotFound,string.Format(UserProfileErrorMessage.UserProfileNotFound,request.UserProfileId));
           return result;
       }
       
       var photo = userProfile.Photos.FirstOrDefault(x => x.Id == request.photoId);
       if (photo is null || photo.IsMain)
       {
           result.AddError(ErrorCode.NotFound,string.Format(UserProfileErrorMessage.CannotDeleteThisPhoto));
           return result;
       }
       
       userProfile.Photos.Remove(photo);
       var photoServiceResult = await _photoService.DeletePhotoAsync(photo.Url);
       await _dataContext.SaveChangesAsync(cancellationToken);
       
       result.PayLoad = new PhotoDto
       {
           Id = photo.Id,
           Url = photo.Url,
           IsMain = photo.IsMain
       };

       return result;
    }
}