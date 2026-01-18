namespace BuildingBlocks.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
                return str;
            return char.ToLowerInvariant(str[0]) + str[1..];
        }

        public static bool IsValidEmail(this string email)
        {
            // Simple email validation - use regex in production
            return email.Contains('@') && email.Contains('.');
        }
    }
}
