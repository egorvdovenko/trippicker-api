using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace demo_web_api.Entities.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<TeacherEntity>
    {
        public void Configure(EntityTypeBuilder<TeacherEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).IsRequired();
            builder.HasOne<TeacherEntity>();
        }
    }
}
