using BuildingBlocks.Common.Base;

namespace WORKMAN.Config.Entites.BussinesRuleConfig
{
    public class BussinesRuleConfig : BaseEntity
    {
        public string RuleName { get; set; } = string.Empty;
        public string RuleDescription { get; set; } = string.Empty;
        public string RuleTitle { get; set; } = string.Empty;
        public int MenuConfigId { get; set; }
        public string RuleTypes { get; set; } = string.Empty;   

    }
}
