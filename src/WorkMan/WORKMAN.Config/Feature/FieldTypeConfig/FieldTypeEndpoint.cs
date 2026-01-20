
using Microsoft.AspNetCore.Mvc;
using WORKMAN.Config.ViewModels.FieldTypeViewModels;

namespace WORKMAN.Config.Feature.FieldTypeConfig
{
    [ApiController]
    [Route("api/config")]
    public class FieldTypeEndpoint : ControllerBase
    {
        private readonly FieldTypeHandler _handler;
        public FieldTypeEndpoint(FieldTypeHandler handler)
        {
            _handler = handler;
        }
        public async Task<ActionResult> AddUpdateFieldType(FieldTypeVM request, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
