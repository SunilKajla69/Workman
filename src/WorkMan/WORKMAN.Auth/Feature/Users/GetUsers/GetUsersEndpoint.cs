namespace WORKMAN.Auth.Feature.Users.CheckProfile
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public sealed class GetUsersEndpoint : ControllerBase
    {
        private readonly GetUsersHandler _handler;
        private readonly ILogger<GetUsersEndpoint> _logger;

        public GetUsersEndpoint(GetUsersHandler handler, ILogger<GetUsersEndpoint> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<UserProfileDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<UserProfileDto>>>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var result = await _handler.HandleAsync(cancellationToken);

            if (result.Count == 0)
            {
                _logger.LogInformation("No user profiles found in database");

                return Ok(ApiResponse<List<UserProfileDto>>.Ok(
                    result,
                    ResponseMessages.UserManagement.UserNotFound,
                    HttpContext.TraceIdentifier));
            }

            _logger.LogInformation("Retrieved {Count} user profiles", result.Count);

            return Ok(ApiResponse<List<UserProfileDto>>.Ok(
                result,
                ResponseMessages.UserManagement.UsersFetched,
                HttpContext.TraceIdentifier));
        }
    }
}
