using DatingApp.Application.UserProfiles.Dtos;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.Contracts.UserProfile.Responses
{
    public record UserProfileResponse
    {
        public Guid UserProfileId { get; set; }
        public BasicInformation BasicInfo { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public List<Photos>? Photos { get;  set; }


        public static UserProfileResponse UserProfileDto(UserProfileDto profile)
        {
            var profileResponse = new UserProfileResponse
            {
                UserProfileId = profile.UserProfileId,
                Photos = profile.Photos,
                DateCreated = profile.DateCreated,
                LastModified = profile.LastModified
            };
            profileResponse.BasicInfo = BasicInformation.UserInfoDto(profile.UserInfo);
            return profileResponse;
        }
    }
}
