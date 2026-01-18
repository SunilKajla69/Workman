namespace WORKMAN.Auth.Feature.Auth.Register
{
    [ApiController]
    [Route("api/auth")]
    public sealed class RegisterEndpoint : ControllerBase
    {
        private readonly RegisterHandler _handler;

        public RegisterEndpoint(RegisterHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<RegisterResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiResponse<RegisterResponse>>> RegisterAsync(
            [FromBody] RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _handler.HandleAsync(request, cancellationToken);
            return Ok(ApiResponse<RegisterResponse>.Ok(
                response,
                ResponseMessages.Auth.RegistrationSuccess,
                HttpContext.TraceIdentifier));
        }
    }
}
