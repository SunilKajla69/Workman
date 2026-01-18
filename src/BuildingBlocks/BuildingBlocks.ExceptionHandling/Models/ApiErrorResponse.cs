namespace BuildingBlocks.ExceptionHandling.Models
{
    /// <summary>
    /// Standardized error response matching the ApiResponse pattern
    /// </summary>
    public sealed class ApiErrorResponse
    {
        public bool Success => false;
        public string TraceId { get; init; } = default!;
        public int StatusCode { get; init; }
        public string Message { get; init; } = default!;
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
        public Dictionary<string, string[]>? Errors { get; init; } // For validation errors
    }
}
