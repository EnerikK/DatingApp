using DatingApp.Application.UserProfiles.Queries;
using System.ComponentModel.DataAnnotations;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.Contracts.Identity
{
    public class UserRegistration
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [MinLength(3)]
        [MaxLength(50)]
        public string KnownAs { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Phone { get; set; }
        
        [Required]
        public string CurrentCity { get; set; }
        
        [MinLength(5)]
        [MaxLength(200)]
        public string Introduction { get; set; }

        [MinLength(5)]
        [MaxLength(200)]
        public string Interests { get; set; }
        
        [MinLength(5)]
        [MaxLength(200)]
        public string LookingFor { get; set; }
        
        public string PhotoUrl { get; set; }

        public List<Photos> Photos { get;  set; }


    }
}
