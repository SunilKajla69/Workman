namespace WORKMAN.UserManagement.Feature.Users.GetUser
{
    public sealed record UserProfileDto
    {
        public long Id { get; init; }
        public string Email { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string? PhoneNumber { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
