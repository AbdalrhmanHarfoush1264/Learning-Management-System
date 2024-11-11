using LMSProject.Data.Data.Enities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class UserRefreshTokenConfig : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshTokens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.User).WithMany(x => x.UserRefreshTokens).HasForeignKey(x => x.UserId);
        }
    }
}
