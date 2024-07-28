using AutoMapper;
using DatingApp.Api.Contracts.UserProfile.Responses;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.MappingProfiles
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<PhotoDto, Photos>();
            CreateMap<BasicInfo, BasicInformation>();
        }
    }
}
