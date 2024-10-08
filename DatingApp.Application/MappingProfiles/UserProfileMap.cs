﻿using AutoMapper;
using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.UserProfiles.Commands;
using DatingApp.Domain.Aggregates.UserProfileAggregates;

namespace DatingApp.Application.MappingProfiles;

internal class UserProfileMap : Profile
{
    public UserProfileMap()
    {
        CreateMap<CreateUserCommand, BasicInfo>();
        CreateMap<Photos, PhotoDto>();
    }
}