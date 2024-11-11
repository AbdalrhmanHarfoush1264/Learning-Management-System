using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class SubmissionConfig : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("Submissions");
            builder.HasKey(x => x.SubmissionId);
            builder.Property(x => x.SubmissionId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Content).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired();
            builder.Property(x => x.SubmissionDate).IsRequired();

            builder.HasOne(x => x.Assignment).WithMany(x => x.Submissions).HasForeignKey(x => x.AssignmentId);
            builder.HasOne(x => x.User).WithMany(x => x.Submissions).HasForeignKey(x => x.UserId);
        }
    }
}