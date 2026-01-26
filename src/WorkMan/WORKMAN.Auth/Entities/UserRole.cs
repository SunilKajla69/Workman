namespace WORKMAN.Auth.Entities
{
    public class UserRole
    {
        public long UserId { get; private set; }
        public User User { get; private set; } = default!;

        public long RoleId { get; private set; }
        public Role Role { get; private set; } = default!;

        private UserRole() { }

        public UserRole(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
