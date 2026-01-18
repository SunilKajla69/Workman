using BuildingBlocks.Common.Base;
using BuildingBlocks.Common.Extensions;
using BuildingBlocks.Common.Guards;

namespace WORKMAN.UserManagement.Entities
{
    public sealed class UserProfile : BaseEntity
    {
        public string Email { get; private set; } // Reference to Auth User
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }

        // Constructor for EF Core
        private UserProfile() { }

        public UserProfile(Guid id, string email, string firstName, string lastName)
        {
            Guard.AgainstNullOrWhiteSpace(email, nameof(email));
            Guard.AgainstNullOrWhiteSpace(firstName, nameof(firstName));
            Guard.AgainstNullOrWhiteSpace(lastName, nameof(lastName));

            email.EnsureValidEmail();
            firstName.EnsureValidName();
            lastName.EnsureValidName();

            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            IsActive = true;
        }

        public void Update(string firstName, string lastName, string? phoneNumber)
        {
            Guard.AgainstNullOrWhiteSpace(firstName, nameof(firstName));
            Guard.AgainstNullOrWhiteSpace(lastName, nameof(lastName));

            firstName.EnsureValidName();
            lastName.EnsureValidName();
            phoneNumber?.EnsureValidPhoneNumber();

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            MarkAsModified();
        }

        public void Deactivate() => IsActive = false;
    }
}
