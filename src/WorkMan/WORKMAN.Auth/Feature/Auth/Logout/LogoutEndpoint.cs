namespace WORKMAN.Auth.Feature.Auth.Logout
{
    [ApiController]
    [Route("api/auth")]
    public sealed class LogoutEndpoint : ControllerBase
    {
        private readonly LogoutHandler _handler;

        public LogoutEndpoint(LogoutHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("logout")]
        [ProducesResponseType(typeof(ApiResponse<LogoutResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<LogoutResponse>>> LogoutAsync(
            [FromBody] LogoutRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _handler.HandleAsync(request, cancellationToken);
            return Ok(ApiResponse<LogoutResponse>.Ok(
                response,
                ResponseMessages.Auth.LogoutSuccess,
                HttpContext.TraceIdentifier));
        }
    }
}
