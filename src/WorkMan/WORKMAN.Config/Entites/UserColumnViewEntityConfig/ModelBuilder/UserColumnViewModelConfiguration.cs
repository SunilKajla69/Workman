namespace WORKMAN.Config.Entites.UserColumnView.ModelBuilder
{
    public class UserColumnViewModelConfiguration : IEntityTypeConfiguration<UserColumnView>
    {
        public void Configure(EntityTypeBuilder<UserColumnView> builder)
        {
            builder.ToTable("UserColumnView");

            // Composite key since UserColumnView doesn't have a single Id property
            builder.HasKey(x => new { x.MenuConfigId, x.RoleId, x.ViewName });

            builder.Property(x => x.ViewName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ViewDisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ViewDiscription)
                .HasMaxLength(1000);

            builder.Property(x => x.ViewTitle)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.MenuConfigId)
                .IsRequired();

            builder.Property(x => x.DisplayOrder)
                .IsRequired();

            builder.Property(x => x.RoleId)
                .IsRequired();

            builder.Property(x => x.IsSelected)
                .IsRequired();

            builder.Property(x => x.ShowFilters)
                .IsRequired();
        }
    }
}
