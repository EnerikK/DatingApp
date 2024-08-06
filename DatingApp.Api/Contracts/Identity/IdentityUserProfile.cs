using DatingApp.Application.Identity.Dtos;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.Contracts.Identity
{
    public class IdentityUserProfile
    {
        public Guid UserProfileId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; }
        public string Token { get; set; }
        public string KnownAs { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string LookingFor { get; set; }
        public int PhotoId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}
