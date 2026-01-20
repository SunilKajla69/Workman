using BuildingBlocks.Common.Base;

namespace WORKMAN.Auth.Entities
{
    public sealed class User : BaseEntity
    {
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;

        private User() { }

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

    public void UpdatePassword(string newPasswordHash, int updatedBy)
    {
        PasswordHash = newPasswordHash;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDeleted(int deletedBy)
    {
        UpdatedBy = deletedBy;
        UpdatedAt = DateTime.UtcNow;
        IsDeleted = 1;
    }
    }
}
