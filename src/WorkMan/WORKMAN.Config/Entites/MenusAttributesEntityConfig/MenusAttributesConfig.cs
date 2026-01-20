using BuildingBlocks.Common.Base;

namespace WORKMAN.Config.Entites.MenusAttributesEntityConfig
{
    public class MenusAttributesConfig : BaseEntity
    {
        public int MenuConfigId { get; set; }
        public string AttributeGroupId { get; set; } = string.Empty;
        public string ColumnName { get; set; } = string.Empty;
        public string AttributeName { get; set; } = string.Empty;
        public string AttributeDisplayName { get; set; } = string.Empty;
        public string AttributeValue { get; set; } = string.Empty;
        public string AttributeDescription { get; set; } = string.Empty;
        public string AttributeTitle { get; set; } = string.Empty;
        public string ImportName{ get; set; } = string.Empty;
        public string ExportName{ get; set; } = string.Empty;
        public int FieldTypeId { get; set; }
        public bool Disable { get; set; }
        public bool IsHide { get; set; }

    }
}
