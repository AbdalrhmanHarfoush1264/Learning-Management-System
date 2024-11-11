using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(x => x.CourseId);
            builder.Property(x => x.CourseId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();
            builder.Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.Level).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Courses).HasForeignKey(x => x.UserId);
        }
    }
}
