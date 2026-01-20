using BuildingBlocks.Common.Base;

namespace WORKMAN.Config.Entites.FilterConfig
{
    public class FilterConfig : BaseEntity
    {
        public string Name { get; set; }
        public string FilterDisplayName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WhereConditionQuery { get; set; }
        public string WhereConditionReplacer { get; set; }
        public int DisplayOrder { get; set; }
    }
}
