namespace WORKMAN.Config.ViewModels.FilterConfigViewModels
{
    public class FilterConfigVM : BaseVM
    {
        public string Name { get; set; } = string.Empty;
        public string FilterDisplayName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string WhereConditionQuery { get; set; } = string.Empty;
        public string WhereConditionReplacer { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
