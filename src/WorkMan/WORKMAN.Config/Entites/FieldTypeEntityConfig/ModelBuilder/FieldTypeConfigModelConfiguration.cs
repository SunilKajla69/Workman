using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WORKMAN.Config.Entites.FeildTypeEntityConfig.ModelBuilder
{
    public class FieldTypeConfigModelConfiguration : IEntityTypeConfiguration<FieldTypeConfig>
    {
        public void Configure(EntityTypeBuilder<FieldTypeConfig> builder)
        {
            builder.ToTable("FieldTypeConfig");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.DisplayOrder)
                .IsRequired();
        }
    }
}
