using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WORKMAN.Config.Entites.BussinesRuleConfig.ModelBuilder
{
    public class BussinesRuleConfigModelConfiguration : IEntityTypeConfiguration<BussinesRuleConfig>
    {
        public void Configure(EntityTypeBuilder<BussinesRuleConfig> builder)
        {
            builder.ToTable("BussinesRuleConfig");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RuleName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.RuleDescription)
                .HasMaxLength(1000);

            builder.Property(x => x.RuleTitle)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.MenuConfigId)
                .IsRequired();

            builder.Property(x => x.RuleTypes)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
