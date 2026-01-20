using BuildingBlocks.Common.Extensions;

namespace WORKMAN.Auth.Entities
{
    public sealed class UserProfile : BaseEntity
    {
        public string Email { get; private set; } = default!;
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }

        private UserProfile() { }

        public UserProfile(long userId, string email)
        {
            Guard.AgainstNullOrWhiteSpace(email, nameof(email));
            email.EnsureValidEmail();

            Id = userId;
            Email = email.Trim().ToLowerInvariant();
            FirstName = null;
            LastName = null;
            IsActive = true;
        }

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
