using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    internal class ForumConfig : IEntityTypeConfiguration<Forum>
    {
        public void Configure(EntityTypeBuilder<Forum> builder)
        {
            builder.ToTable("Forums");
            builder.HasKey(x => x.ForumId);
            builder.Property(x => x.ForumId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnName("Title").HasMaxLength(300).IsRequired();

            builder.HasOne(x => x.Course).WithMany(x => x.Forums).HasForeignKey(x => x.CourseId);
        }
    }
}
