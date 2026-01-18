namespace WORKMAN.UserManagement.Feature.Users.GetUser
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public sealed class GetUserEndpoint : ControllerBase
    {
        private readonly GetUserHandler _handler;

        public GetUserEndpoint(GetUserHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> GetUserAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            // Authorization check: users can only get their own profile
            var currentUserId = Guid.Parse(User.FindFirst("sub")?.Value ?? "");
            if (currentUserId != id)
            {
                return Forbid();
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
