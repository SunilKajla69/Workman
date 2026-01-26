namespace WORKMAN.Config.Entites.MenuConfigLogsEntityConfig.ModelBuilder
{
    public class MenuConfigLogsModelConfiguration : IEntityTypeConfiguration<MenuConfigLogs>
    {
        public void Configure(EntityTypeBuilder<MenuConfigLogs> builder)
        {
            builder.ToTable("MenuConfigLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MenuConfigId)
                .IsRequired();

            builder.Property(x => x.ColumnName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PreviousValue)
                .HasMaxLength(4000);

            builder.Property(x => x.NewValue)
                .HasMaxLength(4000);

            builder.Property(x => x.CreatedBy)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();
        }
    }
}
