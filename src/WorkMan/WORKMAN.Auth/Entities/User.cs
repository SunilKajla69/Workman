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

        private readonly List<UserRole> _userRoles = new();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

        public void AssignRole(Role role)
        {
            if (_userRoles.Any(ur => ur.RoleId == role.Id))
                return;
            _userRoles.Add(new UserRole(Id, role.Id));
        }
    }
}
