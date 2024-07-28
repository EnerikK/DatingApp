using DatingApp.Domain.Aggregates.UserProfileAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Application.Identity.Dtos;

namespace DatingApp.Application.UserProfiles.Dtos
{
    public class UserProfileDto
    {
        public Guid UserProfileId { get; set; }
        public UserInfoDto? UserInfo { get; set; }

        public static UserProfileDto FromUserProfile(UserProfile profile)
        {
            var userProfileDto = new UserProfileDto
            {
                UserProfileId = profile.UserProfileId
            };
            userProfileDto.UserInfo = UserInfoDto.FromBasicInfo(profile.BasicInfo);

            return userProfileDto;
        }
    }
}
