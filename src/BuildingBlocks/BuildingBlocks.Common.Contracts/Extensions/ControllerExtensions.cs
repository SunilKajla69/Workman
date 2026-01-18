using BuildingBlocks.Common.Contracts.Responses;
using BuildingBlocks.Common.Contracts.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Common.Contracts.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ToApiResponse<T>(
        this Result<T> result,
        HttpContext httpContext,
        string? successMessage = null)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(
                    ApiResponse<T>.Ok(result.Value!, successMessage, httpContext.TraceIdentifier));
            }

            return new BadRequestObjectResult(
                ApiResponse<T>.Fail(result.Error!, httpContext.TraceIdentifier));
        }

        public static IActionResult ToApiResponse(
            this Result result,
            HttpContext httpContext,
            string? successMessage = null)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(
                    ApiResponse.Ok(successMessage, httpContext.TraceIdentifier));
            }

            return new BadRequestObjectResult(
                ApiResponse.Fail(result.Error!, httpContext.TraceIdentifier));
        }
    }
}
