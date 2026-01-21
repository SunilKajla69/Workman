namespace WORKMAN.Config.Entites.StatusEntityConfig.ModelBuilder
{
    public class StatusConfigModelConfiguration : IEntityTypeConfiguration<StatusConfig>
    {
        public void Configure(EntityTypeBuilder<StatusConfig> builder)
        {
            builder.ToTable("StatusConfig");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Discription)
                .HasMaxLength(1000);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(300);

            // One StatusConfig has many MenuConfigs
            //builder.HasMany<MenuConfig>()
            //    .WithOne()
            //    .HasForeignKey(m => m.StatusId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
