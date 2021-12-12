using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_web_api.Configuration;
using demo_web_api.Entities;
using demo_web_api.Entities.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace demo_web_api
{
    public class DemoDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public DemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TeacherConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
