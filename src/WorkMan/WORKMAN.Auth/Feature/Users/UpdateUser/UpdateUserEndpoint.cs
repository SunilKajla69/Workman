namespace WORKMAN.Auth.Feature.Users.UpdateUser
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public sealed class UpdateUserEndpoint : ControllerBase
    {
        private readonly UpdateUserHandler _handler;
        private readonly ILogger<UpdateUserEndpoint> _logger;

        public UpdateUserEndpoint(UpdateUserHandler handler, ILogger<UpdateUserEndpoint> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> UpdateUserAsync(
            [FromRoute] long id,
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellationToken)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(currentUserId) || !long.TryParse(currentUserId, out var parsedUserId))
            {
                _logger.LogWarning("Invalid or missing 'sub' claim in JWT");
                return Forbid();
            }

            if (parsedUserId != id)
            {
                _logger.LogWarning(
                    "Authorization failed: User {CurrentUserId} attempted to update profile {TargetUserId}",
                    parsedUserId, id);
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    ApiResponse.Fail(
                        ResponseMessages.UserManagement.OwnProfileOnly,
                        HttpContext.TraceIdentifier));
            }

            try
            {
                var result = await _handler.HandleAsync(id, request, cancellationToken);

                return Ok(ApiResponse<UserProfileDto>.Ok(
                    result,
                    ResponseMessages.UserManagement.ProfileUpdated,
                    HttpContext.TraceIdentifier));
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("not found"))
            {
                _logger.LogWarning(ex, "Profile not found for UserId: {UserId}", id);
                return NotFound(ApiResponse.Fail(
                    ResponseMessages.UserManagement.UserNotFound,
                    HttpContext.TraceIdentifier));
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("inactive"))
            {
                _logger.LogWarning(ex, "Attempted to update inactive profile: {UserId}", id);
                return BadRequest(ApiResponse.Fail(
                    ResponseMessages.UserManagement.CannotUpdateInactive,
                    HttpContext.TraceIdentifier));
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation failed for UserId: {UserId}", id);
                return BadRequest(ApiResponse.Fail(
                    ex.Message,
                    HttpContext.TraceIdentifier));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Business logic error updating UserId: {UserId}", id);
                return BadRequest(ApiResponse.Fail(
                    ex.Message,
                    HttpContext.TraceIdentifier));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating UserId: {UserId}", id);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ApiResponse.Fail(
                        ResponseMessages.General.UnexpectedError,
                        HttpContext.TraceIdentifier));
            }
        }
    }
}
