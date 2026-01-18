namespace BuildingBlocks.Common.Contracts.Responses
{
    public sealed class PagedApiResponse<T>
    {
        public bool Success { get; init; }
        public IReadOnlyList<T> Data { get; init; }
        public PaginationMetadata Pagination { get; init; }
        public string? Message { get; init; }
        public DateTime Timestamp { get; init; }
        public string? TraceId { get; init; }

        private PagedApiResponse(
            bool success,
            IReadOnlyList<T> data,
            PaginationMetadata pagination,
            string? message,
            string? traceId
            )
        {
            Success = success;
            Data = data;
            Pagination = pagination;
            Message = message;
            Timestamp = DateTime.UtcNow;
            TraceId = traceId;
        }

        public static PagedApiResponse<T> Ok(
        IReadOnlyList<T> data,
        int pageNumber,
        int pageSize,
        int totalCount,
        string? message = null,
        string? traceId = null)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var pagination = new PaginationMetadata
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasPrevious = pageNumber > 1,
                HasNext = pageNumber < totalPages
            };

            return new(true, data, pagination, message ?? "Request completed successfully", traceId);
        }
    }

    public sealed record PaginationMetadata
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }
        public bool HasPrevious { get; init; }
        public bool HasNext { get; init; }
    }
}
