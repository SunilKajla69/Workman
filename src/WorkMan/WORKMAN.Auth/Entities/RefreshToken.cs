using BuildingBlocks.Common.Base;

namespace WORKMAN.Auth.Entities
{
    public sealed class RefreshToken : BaseEntity
    {
        public long UserId { get; private set; }
        public string Token { get; private set; } = default!;

        public DateTime ExpiresAtUtc { get; private set; }
        public DateTime? RevokedAtUtc { get; private set; }

        public bool IsActive =>
            RevokedAtUtc is null && DateTime.UtcNow < ExpiresAtUtc;

        private RefreshToken() { }

        public RefreshToken(long userId, string token, DateTime expiresAtUtc)
        {
            UserId = userId;
            Token = token;
            ExpiresAtUtc = expiresAtUtc;
        }

    public void Revoke(int revokedBy)
    {
        RevokedAtUtc = DateTime.UtcNow;
        UpdatedBy = revokedBy;
        UpdatedAt = DateTime.UtcNow;
    }
    }
}
