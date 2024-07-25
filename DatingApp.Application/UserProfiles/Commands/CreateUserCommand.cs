using DatingApp.Application.Models;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;

namespace DatingApp.Application.UserProfiles.Commands;

public class CreateUserCommand : IRequest<OperationResult<UserProfile>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; } // maybe i remove email and phone latter 
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CurrentCity { get; set; }
}