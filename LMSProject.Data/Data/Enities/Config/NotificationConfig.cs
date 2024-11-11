using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.NotificationId);
            builder.Property(x => x.NotificationId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Message).HasMaxLength(1000).HasColumnType("VARCHAR").IsRequired();
            builder.Property(x => x.SendDate).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Notifications).HasForeignKey(x => x.UserId);
        }
    }
}

