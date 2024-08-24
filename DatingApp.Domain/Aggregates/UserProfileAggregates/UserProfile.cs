using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Domain.Extensions;

namespace DatingApp.Domain.Aggregates.UserProfileAggregates
{
    public class UserProfile
    {
        public UserProfile()
        {
            Photos = new List<Photos>();
            LikedByUsers = new List<UserLike>();
            LikedUsers = new List<UserLike>();
        }

        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }
        public List<Photos>? Photos { get; private set; }
        public List<UserLike> LikedByUsers { get; private set; } 
        public List<UserLike> LikedUsers { get; private set; }

        //Factory Method
        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
            return new UserProfile
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Photos = new List<Photos>(),
                LikedByUsers = new List<UserLike>(),
                LikedUsers = new List<UserLike>()
            };
        }
        
        //Public Method 
        public void AddLike(UserLike like)
        {
            if (like != null && !LikedUsers.Any(l => l.TargetUserId == like.TargetUserId))
            {
                LikedUsers.Add(like);
                like.TargetUser?.LikedByUsers.Add(like);
                LastModified = DateTime.UtcNow;
                like.TargetUser.LastModified = DateTime.UtcNow;
            }
        }
        public void DeleteLike(Guid targetUserId)
        {
            var like = LikedUsers.FirstOrDefault(l => l.TargetUserId == targetUserId);
            if (like != null)
            {
                LikedUsers.Remove(like);
                like.TargetUser.LikedByUsers.Remove(like);
                LastModified = DateTime.UtcNow;
                like.TargetUser.LastModified = DateTime.UtcNow;
            }
        }
        public int GetAge()
        {
            return BasicInfo.DateOfBirth.CalculateAge();
        }
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
