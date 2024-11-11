using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class AssignmentConfig : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignments");
            builder.HasKey(x => x.AssignmentId);
            builder.Property(x => x.AssignmentId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();
            builder.Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasOne(x => x.Course).WithMany(x => x.Assignments).HasForeignKey(x => x.CourseId);
        }
    }
}

