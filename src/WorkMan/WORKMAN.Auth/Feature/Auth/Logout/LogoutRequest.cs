namespace WORKMAN.Auth.Feature.Auth.Logout
{
    public sealed class LogoutRequest
    {
        public string RefreshToken { get; init; } = default!;
    }
}
