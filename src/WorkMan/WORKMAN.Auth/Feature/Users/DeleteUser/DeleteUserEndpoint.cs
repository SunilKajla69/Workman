namespace WORKMAN.Auth.Feature.Users.DeleteUser
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public sealed class DeleteUserEndpoint : ControllerBase
    {
        private readonly DeleteUserHandler _handler;

        public DeleteUserEndpoint(DeleteUserHandler handler)
        {
            _handler = handler;
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser(
            long id,
            CancellationToken cancellationToken)
        {
            var currentUserIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(currentUserIdClaim) ||
                !long.TryParse(currentUserIdClaim, out var currentUserId))
            {
                return Forbid();
            }

            var result = await _handler.Handle(
                targetUserId: id,
                currentUserId: currentUserId,
                cancellationToken);

            if (result.NotFound)
            {
                return NotFound(ApiResponse.Fail(
                    ResponseMessages.UserManagement.UserNotFound,
                    HttpContext.TraceIdentifier));
            }

            return Ok(ApiResponse.Ok(
                ResponseMessages.UserManagement.UserDeleted,
                HttpContext.TraceIdentifier));
        }
    }
}
