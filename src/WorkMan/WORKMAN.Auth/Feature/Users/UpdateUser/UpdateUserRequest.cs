namespace WORKMAN.Auth.Feature.Users.UpdateUser
{
    public sealed record UpdateUserRequest
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string? PhoneNumber { get; init; }
    }
}
