namespace WORKMAN.Config.ViewModels.FieldTypeViewModels
{
    public class FieldTypeVM
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Discription { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public string FieldType { get; set; }
        public string Icon { get; set; }
    }
}
