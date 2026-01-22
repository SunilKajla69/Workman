using BuildingBlocks.Common.Base;

namespace WORKMAN.Config.Entites.MenuEntityConfig
{
    public class MenuConfig: BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string MenuName { get; set; } = string.Empty;
        public string MenuDisplayName { get; set; } = string.Empty;
        public string MenuTitle { get; set; } = string.Empty;
        public string MenuIcon { get; set; } = string.Empty;
        public string MenuPath { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;
        public int ParentMenuId { get; set; }
        public int MenuTypeId { get; set; }
        public string MenuDiscription { get; set; } = string.Empty;
        public int StatusId{ get; set; }
        public int DisplayOrder{ get; set; }
        public bool IsCreatedBySystem { get; set; }

    }
}
