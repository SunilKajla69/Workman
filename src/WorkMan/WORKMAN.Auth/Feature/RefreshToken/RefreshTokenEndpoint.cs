namespace WORKMAN.Auth.Feature.RefreshToken
{
    [ApiController]
    [Route("api/auth")]
    public class RefreshTokenEndpoint : ControllerBase
    {
        private readonly RefreshTokenHandler _handler;

        public RefreshTokenEndpoint(RefreshTokenHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(ApiResponse<RefreshTokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh(
            [FromBody] RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _handler.HandleAsync(request, cancellationToken);
            return Ok(ApiResponse<RefreshTokenResponse>.Ok(
                response,
                ResponseMessages.Auth.TokenRefreshSuccess,
                HttpContext.TraceIdentifier));
        }
    }
}
