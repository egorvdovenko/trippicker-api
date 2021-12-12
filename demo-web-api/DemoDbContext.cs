using Microsoft.EntityFrameworkCore;
using demo_web_api.Entities;
using demo_web_api.Entities.Configurations;
using demo_web_api.Entities.ManyToMany;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace demo_web_api
{
    public class DemoDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public DemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<PlaceEntity> Places { get; set; }
        public DbSet<PlaceTagEntity> PlaceTags { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PlaceConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());
            builder.ApplyConfiguration(new PlaceTagConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
