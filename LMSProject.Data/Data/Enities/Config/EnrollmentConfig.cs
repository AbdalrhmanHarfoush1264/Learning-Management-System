using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollments");
            builder.HasKey(x => x.EnrollmentId);
            builder.Property(x => x.EnrollmentId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.EnrollmentDate).IsRequired();

            builder.HasOne(x => x.Course).WithMany(x => x.Enrollments).HasForeignKey(x => x.CourserId);
            builder.HasOne(x => x.User).WithMany(x => x.Enrollments).HasForeignKey(x => x.UserId);
        }
    }
}