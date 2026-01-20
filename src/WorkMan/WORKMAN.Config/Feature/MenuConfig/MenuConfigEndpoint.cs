using BuildingBlocks.Common.Contracts.Messages;
using BuildingBlocks.Common.Contracts.Responses;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace WORKMAN.Config.Feature.MenuConfig
{
    [ApiController]
    [Route("api/menuconfig")]
    public class MenuConfigEndpoint : ControllerBase
    {
        //public async Task<ActionResult<ApiResponse<string>>> LoginAsync(
        //  [FromBody] LoginRequest request,
        //  CancellationToken cancellationToken)
        //{
        //    var response = await _handler.HandleAsync(request, cancellationToken);
        //    return Ok(ApiResponse<LoginResponse>.Ok(
        //        response,
        //        ResponseMessages.Auth.LoginSuccess,
        //        HttpContext.TraceIdentifier));
        //}
    }
}
