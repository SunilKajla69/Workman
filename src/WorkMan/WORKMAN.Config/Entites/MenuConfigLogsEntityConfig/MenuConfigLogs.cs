namespace WORKMAN.Config.Entites.MenuConfigLogsEntityConfig
{
    public class MenuConfigLogs
    {
        public int Id { get; set; }
        public int MenuConfigId { get; set; }
        public string ColumnName { get; set; } = string.Empty;
        public string Action { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
