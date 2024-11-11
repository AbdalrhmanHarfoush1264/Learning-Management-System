using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSProject.Data.Data.Enities.Config
{
    public class ModuleConfig : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules");
            builder.HasKey(x => x.ModuleId);
            builder.Property(x => x.ModuleId).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasColumnType("VARCHAR").HasMaxLength(300).IsRequired();
            builder.Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(500).IsRequired(false);

            builder.HasOne(x => x.Course).WithMany(x => x.Modules).HasForeignKey(x => x.CourseId);
        }
    }
}

