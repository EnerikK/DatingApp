using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.UserProfiles.Dtos;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.Contracts.UserProfile.Responses
{
    //Data Models
    public class BasicInformation
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CurrentCity { get; set; }
        public string? PhotoUrl { get; set; }
        public string? KnownAs { get; set; }
        public string? Introduction { get; set; }
        public string? Interests { get; set; }
        public string? LookingFor { get; set; }
        public static BasicInformation UserInfoDto (UserInfoDto infoDto)
        {
            return new BasicInformation
            {
                FirstName = infoDto.FirstName,
                LastName = infoDto.LastName,
                EmailAddress = infoDto.EmailAddress,
                Phone = infoDto.Phone,
                DateOfBirth = infoDto.DateOfBirth,
                CurrentCity = infoDto.CurrentCity,
                Introduction = infoDto.Introduction,
                KnownAs = infoDto.KnownAs,
                Interests = infoDto.Interests,
                LookingFor = infoDto.LookingFor,
                PhotoUrl = infoDto.PhotoUrl
            };
        }
    }
}
