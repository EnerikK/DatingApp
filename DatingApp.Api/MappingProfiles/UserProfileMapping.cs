using AutoMapper;
using DatingApp.Api.Contracts.UserProfile.Requests;
using DatingApp.Api.Contracts.UserProfile.Responses;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.UserProfiles.Commands;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.MappingProfiles
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<PhotoDto, Photos>()
                .ForMember(dest => dest.Id, opt
                    => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Url, opt
                    => opt.MapFrom(src => src.Url))           
                .ForMember(dest => dest.IsMain, opt
                    => opt.MapFrom(src => src.IsMain));
            CreateMap<BasicInfo, BasicInformation>();
            CreateMap<UserProfileCreateUpdate, UpdateUserProfileBasicInfo>();

        }
    }
}
