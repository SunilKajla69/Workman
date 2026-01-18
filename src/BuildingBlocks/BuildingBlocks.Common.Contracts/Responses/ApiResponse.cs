namespace BuildingBlocks.Common.Contracts.Responses
{
    public sealed class ApiResponse<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public string? Message { get; init; }
        public DateTime Timestamp { get; init; }
        public string? TraceId { get; init; }

        private ApiResponse(bool success, T? data, string? message, string? traceId)
        {
            Success = success;
            Data = data;
            Message = message;
            Timestamp = DateTime.UtcNow;
            TraceId = traceId;
        }

        /// <summary>
        /// Creates a successful response with data
        /// </summary>
        public static ApiResponse<T> Ok(T data, string? message = null, string? traceId = null)
            => new(true, data, message ?? "Request completed successfully", traceId);

        /// <summary>
        /// Creates a successful response without data
        /// </summary>
        public static ApiResponse<T> Ok(string? message = null, string? traceId = null)
            => new(true, default, message ?? "Request completed successfully", traceId);

        /// <summary>
        /// Creates a failure response
        /// </summary>
        public static ApiResponse<T> Fail(string message, string? traceId = null)
            => new(false, default, message, traceId);
    }

    /// <summary>
    /// Non-generic version for operations without return data
    /// </summary>
    public sealed class ApiResponse
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public DateTime Timestamp { get; init; }
        public string? TraceId { get; init; }

        private ApiResponse(bool success, string? message, string? traceId)
        {
            Success = success;
            Message = message;
            Timestamp = DateTime.UtcNow;
            TraceId = traceId;
        }

        public static ApiResponse Ok(string? message = null, string? traceId = null)
            => new(true, message ?? "Request completed successfully", traceId);

        public static ApiResponse Fail(string message, string? traceId = null)
            => new(false, message, traceId);
    }
}
