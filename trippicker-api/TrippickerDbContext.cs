using Microsoft.EntityFrameworkCore;
using trippicker_api.Entities;
using trippicker_api.Entities.Configurations;
using trippicker_api.Entities.ManyToMany;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace trippicker_api
{
    public class TrippickerDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public TrippickerDbContext(DbContextOptions options) : base(options)
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
