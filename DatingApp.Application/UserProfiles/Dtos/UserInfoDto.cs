using DatingApp.Domain.Aggregates.UserProfileAggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Application.Identity.Dtos;

namespace DatingApp.Application.UserProfiles.Dtos
{
    public class UserInfoDto
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? EmailAddress { get; private set; }
        public string? Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string? CurrentCity { get; private set; }
        public string KnownAs { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string LookingFor { get; set; }
        public int PhotoId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public static UserInfoDto FromBasicInfo(BasicInfo basicInfo)
        {
            return new UserInfoDto
            {
                FirstName = basicInfo.FirstName,
                LastName = basicInfo.LastName,
                EmailAddress = basicInfo.EmailAddress,
                Phone = basicInfo.Phone,
                DateOfBirth = basicInfo.DateOfBirth,
                CurrentCity = basicInfo.CurrentCity,
                KnownAs = basicInfo.KnownAs,
                Introduction = basicInfo.Introduction,
                Interests = basicInfo.Interests,
                LookingFor = basicInfo.LookingFor,
                PhotoId = basicInfo.PhotoId,
                Url = basicInfo.Url,
                IsMain = basicInfo.IsMain
            };
        }
    }
}
