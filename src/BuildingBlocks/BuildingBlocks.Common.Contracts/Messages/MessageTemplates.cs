namespace BuildingBlocks.Common.Contracts.Messages
{
    public static class MessageTemplates
    {
        public static class Auth
        {
            public static string UserLockedUntil(DateTime unlockTime)
                => $"Account is locked until {unlockTime:yyyy-MM-dd HH:mm} UTC";

            public static string PasswordExpiresIn(int days)
                => $"Your password will expire in {days} day(s)";

            public static string MaxLoginAttempts(int attempts)
                => $"Maximum login attempts ({attempts}) exceeded";
        }

        public static class Validation
        {
            public static string FieldRequired(string fieldName)
                => $"{fieldName} is required";

            public static string InvalidLength(string fieldName, int minLength, int maxLength)
                => $"{fieldName} must be between {minLength} and {maxLength} characters";

            public static string MustBeUnique(string fieldName)
                => $"{fieldName} must be unique";
        }
    }
}
