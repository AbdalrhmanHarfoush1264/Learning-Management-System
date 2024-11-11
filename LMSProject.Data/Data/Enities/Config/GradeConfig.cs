using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class GradeConfig : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");
            builder.HasKey(x => x.GradeId);
            builder.Property(x => x.GradeId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.grade).IsRequired();

            builder.HasOne(x => x.Submission).WithOne(x => x.grade).HasForeignKey<Grade>(x => x.SubmissionId);
        }
    }
}
