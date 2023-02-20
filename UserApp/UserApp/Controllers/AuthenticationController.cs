using AppUser.BusinessServices.DTO;
using AppUser.BusinessServices.IServices;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AppUser.API.Controllers
{
    /// <summary>
    /// AuthenticationController
    /// </summary>
    public class AuthenticationController : BaseApiController
    {
        #region fields
        private readonly IUserService _userService;
        #endregion


        #region ctor
        /// <summary>
        /// AuthenticationController Constructor
        /// </summary>
        /// <param name="userService"></param>
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion


        #region authenticate user
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate([FromBody, Required] LoginDTO loginDTO)
        {
            Log.Information("Authenticate (AuthController)");
            var token = await _userService.AuthenticateUser(loginDTO);

            return Ok(token);
        }
        #endregion
    }
}
