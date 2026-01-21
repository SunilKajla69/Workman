namespace WORKMAN.Config.Entites.AttributeGroupConfig
{
    public class AttributeGroup: BaseEntity
    {
        public int MenuConfigId { get; set; }
        public string GroupName{ get; set; } = string.Empty;
        public string GroupDisplayName{ get; set; } = string.Empty;
        public string GroupTitle { get; set; } = string.Empty;
        public string GroupDescription { get; set; } = string.Empty;
        public int GroupDisplayOrder { get; set; }

    }
}
