namespace WORKMAN.Auth.Feature.Auth.Register
{
    public sealed record RegisterResponse
    {
        public long UserId { get; init; }
        public string Email { get; init; } = default!;
    }
}
