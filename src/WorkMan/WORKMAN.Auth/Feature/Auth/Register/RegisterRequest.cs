namespace WORKMAN.Auth.Feature.Auth.Register
{
    public sealed record RegisterRequest
    {
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string? FirstName { get; init; }
        public string? LastName { get; init; } 
    }
}
