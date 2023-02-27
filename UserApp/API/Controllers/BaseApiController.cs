using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserApp.BusinessServices.DTO;

namespace UserApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [SwaggerResponse(statusCode: StatusCodes.Status401Unauthorized, type: typeof(ErrorDetails))]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorDetails))]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorDetails))]
    public class BaseApiController : ControllerBase
    {

    }
}
