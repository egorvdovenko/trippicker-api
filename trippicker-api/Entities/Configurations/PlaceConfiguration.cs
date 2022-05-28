using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace trippicker_api.Entities.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<PlaceEntity>
    {
        public void Configure(EntityTypeBuilder<PlaceEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Longitude).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
