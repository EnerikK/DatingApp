using DatingApp.Application.UserProfiles.Dtos;

namespace DatingApp.Api.Contracts.UserProfile.Responses
{
    public record UserProfileResponse
    {
        public Guid UserProfileId { get; set; }
        public BasicInformation BasicInfo { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        public static UserProfileResponse UserProfileDto(UserProfileDto profile)
        {
            var profileResponse = new UserProfileResponse { UserProfileId = profile.UserProfileId };
            profileResponse.BasicInfo = BasicInformation.UserInfoDto(profile.UserInfo);
            return profileResponse;
        }
    }
}
