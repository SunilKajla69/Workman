namespace WORKMAN.Config.ViewModels.UserColumnViewViewModels
{
    public class UserColumnViewVM
    {
        public string ViewName { get; set; } = string.Empty;
        public string ViewDisplayName { get; set; } = string.Empty;
        public string ViewDiscription { get; set; } = string.Empty;
        public string ViewTitle { get; set; } = string.Empty;
        public string MenuConfigId { get; set; }
        public int DisplayOrder { get; set; }
        public string RoleId { get; set; }
        public bool IsSelected { get; set; }
        public bool ShowFilters { get; set; }
    }
}
