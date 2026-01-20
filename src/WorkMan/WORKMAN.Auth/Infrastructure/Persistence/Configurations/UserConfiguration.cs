namespace WORKMAN.Auth.Infrastructure.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityByDefaultColumn();
                //.ValueGeneratedOnAdd(); 

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.HasIndex(x => x.Email)
                .IsUnique(); 

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            // BaseEntity properties
            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.CreatedBy)
                .HasDefaultValue(0);

            builder.Property(x => x.UpdatedBy)
                .HasDefaultValue(0);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(0);

            // Soft delete filter
            builder.HasQueryFilter(x => x.IsDeleted == 0);
        }
    }
}
