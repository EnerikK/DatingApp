using DatingApp.Application.Models;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Application.UserProfiles.Helper;

namespace DatingApp.Application.UserProfiles.Queries
{
    public class GetAllUserProfiles : IRequest<OperationResult<PagedList<UserProfile>>>
    {
        public UserParams userParams;
        
    }
}
