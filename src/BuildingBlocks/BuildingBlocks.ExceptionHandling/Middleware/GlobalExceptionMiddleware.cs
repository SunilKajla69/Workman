namespace BuildingBlocks.ExceptionHandling.Middleware
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionMiddleware(
            RequestDelegate request,
            ILogger<GlobalExceptionMiddleware> logger,
            IHostEnvironment environment
            )
        {
            _request = request;
            _logger = logger;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            var traceId = context.TraceIdentifier;

            _logger.LogError(exception,
                "Unhandled exception | TraceId: {TraceId}",
                traceId);

            var (statusCode, message) = exception switch
            {
                ApiException apiEx => (apiEx.StatusCode, apiEx.Message),
                UnauthorizedAccessException => (
                    StatusCodes.Status401Unauthorized, 
                    "Unauthorized access. Please login to get a valid token."),
                ArgumentException argEx => (
                    StatusCodes.Status400BadRequest,
                    _environment.IsDevelopment() ? argEx.Message : "Invalid request data."),
                InvalidOperationException invOpEx when invOpEx.Message.Contains("not found") => (
                    StatusCodes.Status404NotFound,
                    _environment.IsDevelopment() ? invOpEx.Message : "Resource not found."),
                InvalidOperationException invOpEx when invOpEx.Message.Contains("inactive") => (
                    StatusCodes.Status400BadRequest,
                    _environment.IsDevelopment() ? invOpEx.Message : "Operation not allowed."),
                InvalidOperationException => (
                    StatusCodes.Status409Conflict,
                    _environment.IsDevelopment() ? exception.Message : "A conflict occurred."),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    _environment.IsDevelopment() ? exception.Message : "An unexpected error occurred.")
            };

            var response = new ApiErrorResponse
            {
                TraceId = traceId,
                StatusCode = statusCode,
                Message = message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
