﻿using System.ComponentModel.DataAnnotations;

namespace DatingApp.Api.Contracts.UserProfile.Requests
{
    public record UserProfileCreateUpdate
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; } // maybe i remove email and phone latter 

        public string Phone { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        public string CurrentCity { get; set; }
        public string KnownAs { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string LookingFor { get; set; }
        public int PhotoId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}
