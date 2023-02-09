using AppUser.Controllers;
using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AppCoreAPI.Controllers
{
    public class AuthController : BaseApiController
    {
        #region fields
        private readonly IAuthService _authService;
        #endregion


        #region ctor
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion


        #region authenticate admin
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var token = await _authService.AuthenticateUser(login.Email, login.Password);

                if (token != null)
                {
                    try
                    {
                        return Ok(token);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest("Invalid username or password");
                }
            }
            return BadRequest(ModelState);
        } 
        #endregion
    }
}
