namespace WORKMAN.Config.ViewModels.ResponseVM
{
    public class ResponseVM<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TraceId { get; set; }
        public int TotalRecord{ get; set; }

       
    }
}
