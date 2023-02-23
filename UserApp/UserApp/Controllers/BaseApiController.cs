using AppUser.BusinessServices.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AppUser.API.Controllers
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
