namespace WORKMAN.Auth.Infrastructure.Persistence.Configurations
{
    public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd(); // BIGSERIAL for PostgreSQL, INTEGER AUTOINCREMENT for SQLite

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Token)
                .IsRequired();

            builder.HasIndex(x => x.Token)
                .IsUnique(); // Prevent replay attacks

            builder.Property(x => x.ExpiresAtUtc)
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

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
