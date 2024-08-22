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
using DatingApp.Application.UserProfiles.Helper;

namespace DatingApp.Application.UserProfiles.QueryHandler
{
    internal class GetAllUserProfilesHandler : IRequestHandler<GetAllUserProfiles, OperationResult<PagedList<UserProfile>>>
    {
        private readonly DataContext _dataContext;
        public GetAllUserProfilesHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<OperationResult<PagedList<UserProfile>>> Handle(GetAllUserProfiles request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedList<UserProfile>>();

            var userParams = request.userParams;
            var userIdToExclude = userParams.UserProfileIdToExclude;
            var minDOB = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDOB = DateTime.Today.AddYears(-userParams.MinAge - 1);
            
            
            var query = _dataContext.UserProfiles
                .Include(x => x.Photos)
                .Where(x => x.UserProfileId != userIdToExclude)
                .AsQueryable();

            if (userParams.Gender != null)
            {
                query = query.Where(x => x.BasicInfo.Gender == userParams.Gender);
            }

            query = query.Where(x => x.BasicInfo.DateOfBirth >= minDOB && x.BasicInfo.DateOfBirth <= maxDOB);

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(x => x.DateCreated),
                _ => query.OrderByDescending(x => x.LastModified)
            };
            
            var pagedList = await PagedList<UserProfile>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);

            result.PayLoad = pagedList;
            return result;

        }
    }
}
