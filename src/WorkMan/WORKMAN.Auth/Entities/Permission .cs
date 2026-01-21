namespace WORKMAN.Auth.Entities
{
    public sealed class Permission : BaseEntity
    {
        public string Code { get; private set; } = default!; // booking:accept

        private Permission() { }

        public Permission(string code)
        {
            Code = code;
        }
    }
}
