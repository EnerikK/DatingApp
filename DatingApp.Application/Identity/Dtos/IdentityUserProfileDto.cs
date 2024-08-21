using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Application.Identity.Dtos
{
    public class IdentityUserProfileDto
    {
        public Guid UserProfileId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? CurrentCity { get; set; }
        public string? Token { get; set; }
        public string? PhotoUrl { get; set; }
        public string? KnownAs { get; set; }
        public string? Introduction { get; set; }
        public string? Interests { get; set; }
        public string? LookingFor { get; set; }
        public string? Gender { get; set; }
        public List<PhotoDto>? Photos { get;  set; }
    }
}
