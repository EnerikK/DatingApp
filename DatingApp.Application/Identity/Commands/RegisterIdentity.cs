using DatingApp.Application.Identity.Dtos;
using DatingApp.Application.Models;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Identity.Commands
{
    public class RegisterIdentity : IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
        public string KnownAs { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string LookingFor { get; set; }
        public int PhotoId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}
