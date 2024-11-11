using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");
            builder.HasKey(x => x.LessonId);
            builder.Property(x => x.LessonId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnType("VARCHAR").HasMaxLength(300).IsRequired();
            builder.Property(x => x.Content).HasColumnType("VARCHAR").HasMaxLength(300).IsRequired(false);

            builder.HasOne(x => x.Module).WithMany(x => x.Lessons).HasForeignKey(x => x.ModuleId);
        }
    }
}
