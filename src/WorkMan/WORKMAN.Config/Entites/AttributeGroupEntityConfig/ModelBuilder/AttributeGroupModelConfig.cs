using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WORKMAN.Config.Entites.AttributeGroupConfig.ModelBuilder
{
    public class AttributeGroupModelConfig : IEntityTypeConfiguration<AttributeGroup>
    {
        public void Configure(EntityTypeBuilder<AttributeGroup> builder)
        {
            builder.ToTable("AttributeGroup");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MenuConfigId)
                .IsRequired();

            builder.Property(x => x.GroupName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.GroupDisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.GroupTitle)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.GroupDescription)
                .HasMaxLength(1000);

            builder.Property(x => x.GroupDisplayOrder)
                .IsRequired();
        }
    }
}
