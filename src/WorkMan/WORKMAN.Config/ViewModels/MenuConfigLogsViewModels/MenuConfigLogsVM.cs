namespace WORKMAN.Config.ViewModels.MenuConfigLogsViewModels
{
    public class MenuConfigLogsVM
    {
        public string Id { get; set; }
        public string MenuConfigId { get; set; }
        public string ColumnName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string PreviousValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
