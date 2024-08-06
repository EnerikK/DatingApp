using DatingApp.Application.Models;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;

namespace DatingApp.Application.UserProfiles.Commands;

public class UpdateUserProfileBasicInfo : IRequest<OperationResult<UserProfile>>
{
    public Guid UserProfileId { get; set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string EmailAddress { get; private set; }
    public string Phone { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string CurrentCity { get; private set; }
    public string KnownAs { get; set; }
    public string Introduction { get; set; }
    public string Interests { get; set; }
    public string LookingFor { get; set; }
    public int PhotoId { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }
}