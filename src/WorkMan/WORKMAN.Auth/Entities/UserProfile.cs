using BuildingBlocks.Common.Base;
using BuildingBlocks.Common.Extensions;
using BuildingBlocks.Common.Guards;

namespace WORKMAN.Auth.Entities
{
    /// <summary>
    /// User profile entity - represents user identity and profile information.
    /// Consolidated into Auth service for single source of truth.
    /// </summary>
    public sealed class UserProfile : BaseEntity
    {
        public string Email { get; private set; } = default!;
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }

        // EF Core requires parameterless constructor
        private UserProfile() { }

        /// <summary>
        /// Creates a minimal user profile during registration.
        /// FirstName and LastName are optional and can be filled later via Update.
        /// </summary>
        public UserProfile(long userId, string email)
        {
            Guard.AgainstNullOrWhiteSpace(email, nameof(email));
            email.EnsureValidEmail();

            Id = userId; // Must match User.Id for 1:1 relationship
            Email = email.Trim().ToLowerInvariant();
            FirstName = null;
            LastName = null;
            IsActive = true;
        }

        /// <summary>
        /// Updates profile information.
        /// Business logic encapsulated here - validation happens at domain level.
        /// </summary>
        public void Update(string firstName, string lastName, string? phoneNumber, int updatedBy)
        {
            Guard.AgainstNullOrWhiteSpace(firstName, nameof(firstName));
            Guard.AgainstNullOrWhiteSpace(lastName, nameof(lastName));

            firstName.EnsureValidName();
            lastName.EnsureValidName();
            phoneNumber?.EnsureValidPhoneNumber();

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PhoneNumber = phoneNumber?.Trim();
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate(int updatedBy)
    {
        IsActive = false;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }
    }
}
