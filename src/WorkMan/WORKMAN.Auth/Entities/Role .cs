namespace WORKMAN.Auth.Entities
{
    public sealed class Role : BaseEntity
    {
        public string Name { get; private set; } = default!;
        private readonly List<UserRole> _users = new();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles;
        private readonly List<RolePermission> _rolePermissions = new();
        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;
        private Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}
