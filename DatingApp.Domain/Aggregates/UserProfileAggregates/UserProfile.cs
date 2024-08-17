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
            Photos = new List<Photos>();
        }

        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }
        public List<Photos>? Photos { get; private set; }

        //Factory Method
        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
            return new UserProfile
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Photos = new List<Photos>()
            };

        }
        //Public Method 
        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            BasicInfo = newInfo;
            LastModified = DateTime.UtcNow;
        }
        public void AddPhoto(Photos photo)
        {
            if (photo != null)
            {
                Photos.Add(photo);
                LastModified = DateTime.UtcNow;
            }
        }
        public void RemovePhoto(string photoId)
        {
            var photo = Photos.Find(p => Equals(p.Id, photoId));
            if (photo != null)
            {
                Photos.Remove(photo);
                LastModified = DateTime.UtcNow;
            }
        }
        public void UpdatePhoto(Photos updatedPhoto)
        {
            var photo = Photos.Find(p => p.Id == updatedPhoto.Id);
            if (photo != null)
            {
                photo.Url = updatedPhoto.Url;
                photo.IsMain = updatedPhoto.IsMain;
                LastModified = DateTime.UtcNow;
            }
        }
    }
}
