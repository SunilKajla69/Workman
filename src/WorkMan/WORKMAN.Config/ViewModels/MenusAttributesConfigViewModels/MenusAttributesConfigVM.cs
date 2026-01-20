using BuildingBlocks.Common.ViewModels.BaseViewModels;

namespace WORKMAN.Config.ViewModels.MenusAttributesConfigViewModels
{
    public class MenusAttributesConfigVM : BaseVM
    {
        public string MenuConfigId { get; set; }
        public string AttributeGroupId { get; set; } = string.Empty;
        public string ColumnName { get; set; } = string.Empty;
        public string AttributeName { get; set; } = string.Empty;
        public string AttributeDisplayName { get; set; } = string.Empty;
        public string AttributeValue { get; set; } = string.Empty;
        public string AttributeDescription { get; set; } = string.Empty;
        public string AttributeTitle { get; set; } = string.Empty;
        public string ImportName { get; set; } = string.Empty;
        public string ExportName { get; set; } = string.Empty;
        public string FieldTypeId { get; set; }
        public bool Disable { get; set; }
        public bool IsHide { get; set; }
    }
}
