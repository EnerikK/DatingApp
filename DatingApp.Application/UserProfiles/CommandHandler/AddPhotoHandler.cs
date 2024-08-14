using DatingApp.Application.Enum;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using DatingApp.Application.Services;
using DatingApp.Application.UserProfiles.Commands;
using DatingApp.DataAccess;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.UserProfiles.CommandHandler;

public class AddPhotoHandler : IRequestHandler<AddPhoto,OperationResult<PhotoDto>>
{
    private readonly DataContext _dataContext;
    private readonly IPhotoService _photoService;
    public AddPhotoHandler(DataContext dataCtx,IPhotoService photoService)
    {
        _dataContext = dataCtx;
        _photoService = photoService;
    }
    public async Task<OperationResult<PhotoDto>> Handle(AddPhoto request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<PhotoDto>();
        
        var userProfile = await _dataContext.UserProfiles
            .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.UserProfileId,cancellationToken: cancellationToken);

        if (userProfile is null)
        {
            result.AddError(ErrorCode.NotFound,string.Format(UserProfileErrorMessage.UserProfileNotFound,request.UserProfileId));
            return result;
        }
        var photoServiceResult = await _photoService.AddPhotoAsync(request.File);
        if (photoServiceResult is null || string.IsNullOrEmpty(photoServiceResult.PublicId))
        {
            result.AddError(ErrorCode.PhotoUploadFailed, "Photo upload failed.");
            return result;
        }

        var photo = new Photos()
        {
            Id = photoServiceResult.PublicId,
            Url = photoServiceResult.SecureUrl.AbsoluteUri
        };

        userProfile.AddPhoto(photo);
        
        await _dataContext.SaveChangesAsync(cancellationToken);
        
        result.PayLoad = new PhotoDto
        {
            Id = photo.Id,
            Url = photo.Url
        };

        return result;
    }
}