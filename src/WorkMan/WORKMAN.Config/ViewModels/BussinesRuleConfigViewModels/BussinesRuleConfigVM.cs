using BuildingBlocks.Common.ViewModels.BaseViewModels;

namespace WORKMAN.Config.ViewModels.BussinesRuleConfigViewModels
{
    public class BussinesRuleConfigVM : BaseVM
    {
        public string RuleName { get; set; } = string.Empty;
        public string RuleDescription { get; set; } = string.Empty;
        public string RuleTitle { get; set; } = string.Empty;
        public string MenuConfigId { get; set; }
    }
}
