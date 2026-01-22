using BuildingBlocks.Common.Contracts.Messages;
using BuildingBlocks.Common.Contracts.Responses;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata;
using WORKMAN.Config.Feature.FieldTypeConfig;
using WORKMAN.Config.ViewModels.AttributeViewModel;
using WORKMAN.Config.ViewModels.MenuConfigViewModels;
using WORKMAN.Config.ViewModels.ResponseVM;

namespace WORKMAN.Config.Feature.MenuConfig
{
    [ApiController]
    [Route("api/menuconfig")]
    public class MenuConfigEndpoint : ControllerBase
    {
        private readonly MenuConfigHandler _handler;

        //public MenuConfigEndpoint(MenuConfigHandler handler)
        //{
        //    _handler = handler;
        //}
        public MenuConfigEndpoint(MenuConfigHandler handler)
        {
            _handler = handler;
        }

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
        [HttpGet("GetMenus")]
        public async Task<ActionResult<ResponseVM<List<MenuConfigVM>>>> GetMenus(CancellationToken cancellationToken)
        {
            var result = await _handler.Getmenus(cancellationToken);
            if (result.Count > 0)
            {
                ResponseVM<List<MenuConfigVM>> apiResponse = new ResponseVM<List<MenuConfigVM>>() { 
                    Success = true, Data = result, Message = "", TraceId = HttpContext.TraceIdentifier
                };
                return Ok(apiResponse);
            }
            return NotFound();
        }

        [HttpPost("SaveUpdateMenu")]
        public async Task<ActionResult<ResponseVM<int>>> SaveUpdateMenu(List<AttributeVM> attributeVMs, CancellationToken cancellationToken)
        {
            var result = await _handler.SaveUpdateMenu(attributeVMs, cancellationToken);
            if (result.Count > 0)
            {
                ResponseVM<List<MenuConfigVM>> apiResponse = new ResponseVM<List<MenuConfigVM>>()
                {
                    Success = true,
                    Data = result,
                    Message = "",
                    TraceId = HttpContext.TraceIdentifier
                };
                return Ok(apiResponse);
            }
            return NotFound();
        }


    }
}
