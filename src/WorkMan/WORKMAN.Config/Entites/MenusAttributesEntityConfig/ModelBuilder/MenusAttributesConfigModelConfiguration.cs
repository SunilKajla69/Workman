using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WORKMAN.Config.Entites.MenusAttributesEntityConfig.ModelBuilder
{
    public class MenusAttributesConfigModelConfiguration : IEntityTypeConfiguration<MenusAttributesConfig>
    {
        public void Configure(EntityTypeBuilder<MenusAttributesConfig> builder)
        {
            builder.ToTable("MenusAttributesConfig");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MenuConfigId)
                .IsRequired();

            builder.Property(x => x.AttributeGroupId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ColumnName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.AttributeName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.AttributeDisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.AttributeValue)
                .HasMaxLength(1000);

            builder.Property(x => x.AttributeDescription)
                .HasMaxLength(1000);

            builder.Property(x => x.AttributeTitle)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.ImportName)
                .HasMaxLength(200);

            builder.Property(x => x.ExportName)
                .HasMaxLength(200);

            builder.Property(x => x.FieldTypeId)
                .IsRequired();

            builder.Property(x => x.Disable)
                .IsRequired();

            builder.Property(x => x.IsHide)
                .IsRequired();
        }
    }
}
