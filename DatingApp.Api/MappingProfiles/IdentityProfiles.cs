using AutoMapper;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Api.MappingProfiles;

public class IdentityProfiles : Profile
{
    public IdentityProfiles()
    {
        CreateMap<UserProfile, IdentityUserProfileDto>()
            .ForMember(dest => dest.Phone, opt
                => opt.MapFrom(src => src.BasicInfo.Phone))
            .ForMember(dest => dest.CurrentCity, opt
                => opt.MapFrom(src => src.BasicInfo.CurrentCity))
            .ForMember(dest => dest.EmailAddress, opt
                => opt.MapFrom(src => src.BasicInfo.EmailAddress))
            .ForMember(dest => dest.FirstName, opt
                => opt.MapFrom(src => src.BasicInfo.FirstName))
            .ForMember(dest => dest.LastName, opt
                => opt.MapFrom(src => src.BasicInfo.LastName))
            .ForMember(dest => dest.DateOfBirth, opt
                => opt.MapFrom(src => src.BasicInfo.DateOfBirth))
            .ForMember(dest => dest.KnownAs, opt
                => opt.MapFrom(src => src.BasicInfo.KnownAs))
            .ForMember(dest => dest.Introduction, opt
                => opt.MapFrom(src => src.BasicInfo.Introduction))
            .ForMember(dest => dest.Interests, opt
                => opt.MapFrom(src => src.BasicInfo.Interests))
            .ForMember(dest => dest.LookingFor, opt
                => opt.MapFrom(src => src.BasicInfo.LookingFor))
            .ForMember(dest => dest.PhotoUrl, opt
                => opt.MapFrom(src => src.Photos.FirstOrDefault(x=> x.IsMain)!.Url))
            .ForMember(dest => dest.Photos, opt
                => opt.MapFrom(src => src.Photos));





    }
}