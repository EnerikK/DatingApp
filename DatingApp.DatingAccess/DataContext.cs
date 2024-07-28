using DatingApp.DataAccess.Configuration;
using DatingApp.Domain.Aggregates.UserProfileAggregates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.DataAccess
{
    public class DataContext : IdentityDbContext
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Photos> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserProfileConfig());
            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());

        }
    }

}
