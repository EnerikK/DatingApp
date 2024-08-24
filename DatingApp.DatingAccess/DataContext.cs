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
        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<BasicInfo>();
            builder.ApplyConfiguration(new UserProfileConfig());
            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());
            
            builder.Entity<UserLike>()
                .HasKey(k => new { k.SourceUserId, k.TargetUserId });

            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<UserLike>()
                .HasOne(s => s.TargetUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.TargetUserId)
                .OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
