namespace WORKMAN.Auth.Entities
{
    public class RolePermission
    {
        public long RoleId { get; private set; }
        public Role Role { get; private set; } = default!;

        public long PermissionId { get; private set; }
        public Permission Permission { get; private set; } = default!;

        private RolePermission() { }

        public RolePermission(long roleId, long permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}
