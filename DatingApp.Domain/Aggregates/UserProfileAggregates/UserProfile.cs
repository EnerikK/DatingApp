using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Aggregates.UserProfileAggregates
{
    public class UserProfile
    {
        public UserProfile()
        {
            
        }

        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }

        public Photos Photo { get; private set; }

        //Factory Method
        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo,Photos photos)
        {
            return new UserProfile
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Photo = photos
            };

        }
        //Public Method 
        public void UpdateBasicInfo(BasicInfo newInfo,Photos newPhoto)
        {
            BasicInfo = newInfo;
            LastModified = DateTime.UtcNow;
            Photo = newPhoto;
        }
    }
}
