namespace WORKMAN.Config.Entites.UserColumnView
{
    public class UserColumnView
    {
        public string ViewName { get; set; } = string.Empty;
        public string ViewDisplayName { get; set; } = string.Empty;
        public string ViewDiscription{ get; set; } = string.Empty;
        public string ViewTitle{ get; set; } = string.Empty;
        public int MenuConfigId { get; set; }
        public int DisplayOrder { get; set; }
        public int RoleId { get; set; }
        public bool IsSelected { get; set; }
        public bool ShowFilters { get; set; } = false;

    }
}
