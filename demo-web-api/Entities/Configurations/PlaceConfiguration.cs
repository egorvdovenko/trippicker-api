using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace demo_web_api.Entities.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<PlaceEntity>
    {
        public void Configure(EntityTypeBuilder<PlaceEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.HasMany<TagEntity>();
        }
    }
}
