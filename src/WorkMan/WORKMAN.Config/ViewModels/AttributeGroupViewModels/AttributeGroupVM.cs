using BuildingBlocks.Common.ViewModels.BaseViewModels;

namespace WORKMAN.Config.ViewModels.AttributeGroupViewModels
{
    public class AttributeGroupVM : BaseVM
    {
        public string MenuConfigId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string GroupDisplayName { get; set; } = string.Empty;
        public string GroupTitle { get; set; } = string.Empty;
        public string GroupDescription { get; set; } = string.Empty;
        public int GroupDisplayOrder { get; set; }
    }
}
