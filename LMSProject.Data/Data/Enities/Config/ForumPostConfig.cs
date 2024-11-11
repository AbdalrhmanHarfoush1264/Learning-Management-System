using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class ForumPostConfig : IEntityTypeConfiguration<ForumPost>
    {
        public void Configure(EntityTypeBuilder<ForumPost> builder)
        {
            builder.ToTable("ForumPosts");
            builder.HasKey(x => x.ForumPostId);
            builder.Property(x => x.ForumPostId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Content).HasColumnName("Content").HasMaxLength(300).IsRequired();
            builder.Property(x => x.PostDate).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.ForumPosts).HasForeignKey(x => x.UserId);
        }
    }
}
