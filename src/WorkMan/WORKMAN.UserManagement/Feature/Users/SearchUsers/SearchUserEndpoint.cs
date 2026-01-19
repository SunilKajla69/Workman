using WORKMAN.UserManagement.Feature.Users.GetUser;

namespace WORKMAN.UserManagement.Feature.Users.SearchUsers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public sealed class SearchUserEndpoint : ControllerBase
    {
        private readonly SearchUserHandler _handler;

        public SearchUserEndpoint(SearchUserHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<List<UserProfileDto>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<UserProfileDto>>>> SearchUsersAsync(
            [FromQuery] string query,
            CancellationToken cancellationToken)
        {
            var results = await _handler.HandleAsync(query, cancellationToken);

            return Ok(ApiResponse<List<UserProfileDto>>.Ok(
                results,
                ResponseMessages.General.Success,
                HttpContext.TraceIdentifier));
        }
    }
}
