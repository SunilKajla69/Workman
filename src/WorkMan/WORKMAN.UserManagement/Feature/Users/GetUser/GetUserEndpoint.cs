namespace WORKMAN.UserManagement.Feature.Users.GetUser
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public sealed class GetUserEndpoint : ControllerBase
    {
        private readonly GetUserHandler _handler;
        private readonly ILogger<GetUserEndpoint> _logger;

        public GetUserEndpoint(GetUserHandler handler, ILogger<GetUserEndpoint> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> GetUserAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            // Log authentication status
            _logger.LogInformation("Request received for UserId: {UserId}", id);
            _logger.LogInformation("User.Identity.IsAuthenticated: {IsAuthenticated}", User.Identity?.IsAuthenticated);
            _logger.LogInformation("User.Identity.Name: {Name}", User.Identity?.Name);
            _logger.LogInformation("Claims count: {ClaimsCount}", User.Claims.Count());
            
            foreach (var claim in User.Claims)
            {
                _logger.LogInformation("Claim: {Type} = {Value}", claim.Type, claim.Value);
            }

            // Authorization check: users can only get their own profile
            // Note: JWT "sub" claim is mapped to ClaimTypes.NameIdentifier by ASP.NET Core
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            _logger.LogInformation("UserId claim value: {UserIdClaim}", currentUserIdClaim ?? "NULL");
            
            if (string.IsNullOrEmpty(currentUserIdClaim) || !Guid.TryParse(currentUserIdClaim, out var currentUserId))
            {
                _logger.LogWarning("Invalid or missing 'sub' claim in JWT");
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    ApiResponse<object>.Fail(
                        "Invalid authentication token",
                        HttpContext.TraceIdentifier));
            }

            if (currentUserId != id)
            {
                _logger.LogWarning(
                    "Authorization failed: User {CurrentUserId} attempted to access profile {TargetUserId}",
                    currentUserId, id);
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    ApiResponse<object>.Fail(
                        "You can only access your own profile",
                        HttpContext.TraceIdentifier));
            }

            var result = await _handler.HandleAsync(id, cancellationToken);

            if (result is null)
            {
                return NotFound(ApiResponse<object>.Fail(
                    ResponseMessages.UserManagement.UserNotFound,
                    HttpContext.TraceIdentifier));
            }

            return Ok(ApiResponse<UserProfileDto>.Ok(
                result,
                ResponseMessages.General.Success,
                HttpContext.TraceIdentifier));
        }
    }
}
