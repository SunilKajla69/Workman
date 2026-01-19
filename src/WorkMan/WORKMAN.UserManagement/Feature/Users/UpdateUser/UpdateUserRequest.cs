namespace WORKMAN.UserManagement.Feature.Users.UpdateUser
{
    /// <summary>
    /// Request model for updating user profile
    /// Immutable by design (init-only properties)
    /// Does NOT include Auth-related fields (email, password)
    /// </summary>
    public sealed record UpdateUserRequest
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string? PhoneNumber { get; init; }
    }
}
