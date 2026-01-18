namespace BuildingBlocks.Common.Guards
{
    public static class Guard
    {
        public static void AgainstNull(object? value, string parameterName)
        {
            if (value is null)
                throw new ArgumentNullException(parameterName);
        }

        public static void AgainstNullOrEmpty(string? value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{parameterName} cannot be null or empty", parameterName);
        }

        public static void AgainstNullOrWhiteSpace(string? value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{parameterName} cannot be null or whitespace", parameterName);
        }
    }
}
