using DatingApp.Api.Contracts.Identity;
using DatingApp.Application.Identity.Commands;
using AutoMapper;
using DatingApp.Application.Identity.Dtos;

namespace DatingApp.Api.MappingProfiles;

public class IdentityMappings : Profile
{
    public IdentityMappings()
    {
        CreateMap<UserRegistration, RegisterIdentity>();
        CreateMap<IdentityUserProfileDto, IdentityUserProfile>();
    }
}