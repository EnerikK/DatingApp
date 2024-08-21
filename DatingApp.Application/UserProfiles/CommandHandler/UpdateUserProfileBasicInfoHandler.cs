using DatingApp.Application.Enum;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using DatingApp.Application.Services;
using DatingApp.Application.UserProfiles.Commands;
using DatingApp.DataAccess;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.UserProfiles.CommandHandler;


internal class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo,OperationResult<UserProfile>>
{
    private readonly DataContext _datactx;
    private readonly IPhotoService _photoService;
    public UpdateUserProfileBasicInfoHandler(DataContext dataCtx ,IPhotoService photoService)
    {
        _datactx = dataCtx;
        _photoService = photoService;

    }
    public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<UserProfile>();
        try
        {
            var userProfile = await _datactx.UserProfiles
                .FirstOrDefaultAsync(userProfile => userProfile.UserProfileId == request.UserProfileId,cancellationToken: cancellationToken);

            if (userProfile is null)
            {
                result.AddError(ErrorCode.NotFound,string.Format(UserProfileErrorMessage.UserProfileNotFound,request.UserProfileId));
            }

            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName
                , request.EmailAddress, request.Phone, request.DateOfBirth, request.CurrentCity,
                request.KnownAs,request.Introduction,request.Interests,request.LookingFor,request.PhotoUrl,request.Gender);

            userProfile.UpdateBasicInfo(basicInfo);

            if (request.File != null)
            {
                var currentPhoto = userProfile.Photos.FirstOrDefault();

                /*if (currentPhoto != null && !string.IsNullOrEmpty(currentPhoto.Id))
                {
                    await _photoService.DeletePhotoAsync(currentPhoto.Id);
                    userProfile.Photos.Remove(currentPhoto); // Remove the old photo from the collection
                }*/
                
                var photoServiceResult = await _photoService.AddPhotoAsync(request.File);
                if (photoServiceResult is null || string.IsNullOrEmpty(photoServiceResult.PublicId))
                {
                    result.AddError(ErrorCode.PhotoUploadFailed, "Photo upload failed.");
                    return result;
                }

                if (!int.TryParse(photoServiceResult.PublicId, out int photoId))
                {
                    result.AddError(ErrorCode.InvalidPhotoId, "The photo ID is invalid.");
                    return result;
                }

                var photo = new Photos()
                {
                    Id = photoId,
                    Url = photoServiceResult.SecureUrl.AbsoluteUri
                };

                userProfile.UpdatePhoto(photo);
            }

            _datactx.UserProfiles.Update(userProfile);
            await _datactx.SaveChangesAsync(cancellationToken);

            result.PayLoad = userProfile;
            return result;
        }
        catch (Exception e)
        {
            result.AddUnknownError(e.Message);
        }

        return result;
    }
}
