using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class CertificateConfig : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.ToTable("Certificates");
            builder.HasKey(x => x.CertificateId);
            builder.Property(x => x.CertificateId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.IssueDate).IsRequired();

            builder.HasOne(x => x.Course).WithMany(x => x.Certificates).HasForeignKey(x => x.CourseId);
            builder.HasOne(x => x.User).WithMany(x => x.Certificates).HasForeignKey(x => x.UserId);
        }
    }
}

