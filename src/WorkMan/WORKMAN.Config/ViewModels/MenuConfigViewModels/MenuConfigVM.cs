using BuildingBlocks.Common.ViewModels.BaseViewModels;

namespace WORKMAN.Config.ViewModels.MenuConfigViewModels
{
    public class MenuConfigVM : BaseVM
    {
        public string Code { get; set; } = string.Empty;
        public string MenuName { get; set; } = string.Empty;
        public string MenuDisplayName { get; set; } = string.Empty;
        public string MenuTitle { get; set; } = string.Empty;
        public string MenuIcon { get; set; } = string.Empty;
        public string MenuPath { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;
        public string ParentMenuId { get; set; }
        public string MenuTypeId { get; set; }
        public string MenuDiscription { get; set; } = string.Empty;
        public string StatusId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
