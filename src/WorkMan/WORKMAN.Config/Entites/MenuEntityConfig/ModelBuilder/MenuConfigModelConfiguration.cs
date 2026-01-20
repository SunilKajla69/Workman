using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace WORKMAN.Config.Entites.MenuEntityConfig.ModelBuilder
{
    public class MenuConfigModelConfiguration : IEntityTypeConfiguration<MenuConfig>
    {
        public void Configure(EntityTypeBuilder<MenuConfig> builder)
        {
            builder.ToTable("MenuConfig");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.MenuName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.MenuDisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.MenuTitle)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.MenuIcon)
                .HasMaxLength(100);

            builder.Property(x => x.MenuPath)
                .HasMaxLength(500);

            builder.Property(x => x.TableName)
                .HasMaxLength(200);

            builder.Property(x => x.Query)
                .HasMaxLength(4000);

            builder.Property(x => x.ParentMenuId)
                .IsRequired();

            builder.Property(x => x.MenuTypeId)
                .IsRequired();

            builder.Property(x => x.MenuDiscription)
                .HasMaxLength(1000);

            builder.Property(x => x.StatusId)
                .IsRequired();

            builder.Property(x => x.DisplayOrder)
                .IsRequired();
            
        }
    }
}
