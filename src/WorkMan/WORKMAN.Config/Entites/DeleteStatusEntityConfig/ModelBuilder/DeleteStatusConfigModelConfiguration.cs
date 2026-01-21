namespace WORKMAN.Config.Entites.DeleteStatusConfig.ModelBuilder
{
    public class DeleteStatusConfigModelConfiguration : IEntityTypeConfiguration<DeleteStatusConfig>
    {
        public void Configure(EntityTypeBuilder<DeleteStatusConfig> builder)
        {
            builder.ToTable("DeleteStatusConfig");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Discription)
                .HasMaxLength(1000);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
