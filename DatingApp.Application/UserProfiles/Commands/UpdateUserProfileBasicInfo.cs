﻿using DatingApp.Application.Models;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;

namespace DatingApp.Application.UserProfiles.Commands;

public class UpdateUserProfileBasicInfo : IRequest<OperationResult<UserProfile>>
{
    public Guid UserProfileId { get; set; }
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public string EmailAddress { get;  set; }
    public string Phone { get;  set; }
    public DateTime DateOfBirth { get;  set; }
    public string CurrentCity { get;  set; }
    public string KnownAs { get; set; }
    public string Introduction { get; set; }
    public string Interests { get; set; }
    public string LookingFor { get; set; }
    public int PhotoId { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
}