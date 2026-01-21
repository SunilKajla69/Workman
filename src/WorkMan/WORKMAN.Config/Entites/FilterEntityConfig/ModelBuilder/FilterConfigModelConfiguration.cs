namespace WORKMAN.Config.Entites.FilterConfig.ModelBuilder
{
    public class FilterConfigModelConfiguration : IEntityTypeConfiguration<FilterConfig>
    {
        public void Configure(EntityTypeBuilder<FilterConfig> builder)
        {
            builder.ToTable("FilterConfig");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.FilterDisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.WhereConditionQuery)
                .HasMaxLength(4000);

            builder.Property(x => x.WhereConditionReplacer)
                .HasMaxLength(1000);

            builder.Property(x => x.DisplayOrder)
                .IsRequired();
        }
    }
}
