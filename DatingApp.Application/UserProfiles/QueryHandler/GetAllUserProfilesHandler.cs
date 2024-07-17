using DatingApp.Application.Models;
using DatingApp.Application.UserProfiles.Queries;
using DatingApp.DataAccess;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.UserProfiles.QueryHandler
{
    internal class GetAllUserProfilesHandler : IRequestHandler<GetAllUserProfiles, OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly DataContext _dataContext;
        public GetAllUserProfilesHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetAllUserProfiles request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<UserProfile>>();
            result.PayLoad = await _dataContext.UserProfiles.ToListAsync(cancellationToken);
            return result;
        }
    }
}
