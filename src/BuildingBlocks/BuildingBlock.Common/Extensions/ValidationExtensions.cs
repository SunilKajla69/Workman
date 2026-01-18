using BuildingBlocks.Common.Guards;

namespace BuildingBlocks.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static void EnsureValidEmail(this string email)
        {
            Guard.AgainstNullOrEmpty(email, nameof(email));

            if (!email.Contains("@") || !email.Contains('.'))
                throw new ArgumentException("Invalid email format", nameof(email));
        }

        public static void EnsureValidPhoneNumber(this string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return; // Optional field

            // Simple validation - customize as needed
            if (phoneNumber.Length < 10 || !phoneNumber.All(char.IsDigit))
                throw new ArgumentException("Invalid phone number format", nameof(phoneNumber));
        }

        public static void EnsureValidName(this string name)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));

            if (name.Length < 2 || name.Length > 50)
                throw new ArgumentException("Name must be between 2 and 50 characters", nameof(name));

            if (!name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                throw new ArgumentException("Name can only contain letters and spaces", nameof(name));
        }
    }
}
