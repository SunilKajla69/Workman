namespace WORKMAN.Auth.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Entity Framework configuration for UserProfile.
    /// Enforces 1:1 relationship with User table via shared primary key.
    /// </summary>
    public sealed class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");

            // Primary key - same as User.Id (1:1 relationship via shared PK)
            builder.HasKey(x => x.Id);

            // No auto-increment - Id comes from User table
            builder.Property(x => x.Id)
                .ValueGeneratedNever(); // ?? Critical: This is NOT auto-generated

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            // Unique index on Email for fast lookups
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.FirstName)
                .IsRequired(false) // Optional - can be filled later via Update Profile
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired(false) // Optional - can be filled later via Update Profile
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20); // Optional field

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

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
