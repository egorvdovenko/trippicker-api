using demo_web_api.Entities.ManyToMany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace demo_web_api.Entities.Configurations
{
    public class PlaceTagConfiguration : IEntityTypeConfiguration<PlaceTagEntity>
    {
        public void Configure(EntityTypeBuilder<PlaceTagEntity> builder)
        {
            builder.ToTable("PlaceTags");
            builder.HasKey(pt => new { pt.PlaceId, pt.TagId });
            builder
                .HasOne(pt => pt.Place)
                .WithMany(p => p.PlaceTags)
                .HasForeignKey(pt => pt.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PlaceTags)
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
