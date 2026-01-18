namespace WORKMAN.Auth.Feature.Auth.Login
{
    [ApiController]
    [Route("api/auth")]
    public class LoginEndpoint : ControllerBase
    {
        private readonly LoginHandler _handler;

        public LoginEndpoint(LoginHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        [EnableRateLimiting("login-policy")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> LoginAsync(
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _handler.HandleAsync(request, cancellationToken);
            return Ok(ApiResponse<LoginResponse>.Ok(
                response,
                ResponseMessages.Auth.LoginSuccess,
                HttpContext.TraceIdentifier));
        }
    }
}
