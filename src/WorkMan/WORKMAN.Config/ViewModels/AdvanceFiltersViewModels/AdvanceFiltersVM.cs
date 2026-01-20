namespace WORKMAN.Config.ViewModels.AdvanceFiltersViewModels
{
    public class AdvanceFiltersVM
    {
        public string FilterName { get; set; } = string.Empty;
        public string FilterDisplayName { get; set; } = string.Empty;
        public string FilterTitle { get; set; } = string.Empty;
        public string FilterDescription { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }
}
