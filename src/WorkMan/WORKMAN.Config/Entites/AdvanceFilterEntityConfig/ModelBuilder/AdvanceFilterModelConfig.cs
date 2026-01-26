namespace WORKMAN.Config.Entites.AdvanceFilterConfig.ModelBuilder
{
    public class AdvanceFilterModelConfig : IEntityTypeConfiguration<AdvanceFilters>
    {
        public void Configure(EntityTypeBuilder<AdvanceFilters> builder)
        {
            builder.ToTable("AdvanceFilters");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FilterName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.FilterDisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.FilterTitle)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.FilterDescription)
                .HasMaxLength(1000);

            builder.Property(x => x.DisplayOrder)
                .IsRequired();
        }
    }
}
