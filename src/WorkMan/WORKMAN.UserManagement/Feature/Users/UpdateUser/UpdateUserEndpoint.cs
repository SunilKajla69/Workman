using WORKMAN.UserManagement.Feature.Users.GetUser;

namespace WORKMAN.UserManagement.Feature.Users.UpdateUser
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

        /// <summary>
        /// Updates user profile information (FirstName, LastName, PhoneNumber)
        /// </summary>
        /// <param name="id">User ID from route</param>
        /// <param name="request">Profile update data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated user profile</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> UpdateUserAsync(
            [FromRoute] Guid id,
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellationToken)
        {
            // Authorization: Users can only update their own profile
            // Note: JWT "sub" claim is mapped to ClaimTypes.NameIdentifier by ASP.NET Core
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(currentUserId) || !Guid.TryParse(currentUserId, out var parsedUserId))
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
                    ApiResponse<object>.Fail(
                        "You can only update your own profile",
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
                return NotFound(ApiResponse<object>.Fail(
                    ResponseMessages.UserManagement.UserNotFound,
                    HttpContext.TraceIdentifier));
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("inactive"))
            {
                _logger.LogWarning(ex, "Attempted to update inactive profile: {UserId}", id);
                return BadRequest(ApiResponse<object>.Fail(
                    "Cannot update inactive user profile",
                    HttpContext.TraceIdentifier));
            }
            catch (ArgumentException ex)
            {
                // Validation errors from Guard or domain logic
                _logger.LogWarning(ex, "Validation failed for UserId: {UserId}", id);
                return BadRequest(ApiResponse<object>.Fail(
                    ex.Message,
                    HttpContext.TraceIdentifier));
            }
            catch (InvalidOperationException ex)
            {
                // General business logic errors
                _logger.LogError(ex, "Business logic error updating UserId: {UserId}", id);
                return BadRequest(ApiResponse<object>.Fail(
                    ex.Message,
                    HttpContext.TraceIdentifier));
            }
            catch (Exception ex)
            {
                // Unexpected errors
                _logger.LogError(ex, "Unexpected error updating UserId: {UserId}", id);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.Fail(
                        ResponseMessages.General.UnexpectedError,
                        HttpContext.TraceIdentifier));
            }
        }
    }
}
