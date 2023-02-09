using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace AppUser.Controllers
{
    public class UserController : BaseApiController
    {
        #region fields
        private readonly IUserService _userService;
        #endregion


        #region ctor
        public UserController(IAuthService authService, IUserService userService)
        {
            _userService = userService;
        } 
        #endregion


        #region add user api
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerResponse(statusCode: StatusCodes.Status201Created, type: typeof(UserDTO))]

        public async Task<IActionResult> AddUser(UserDTO obj)
        {
            Log.Information($"AddUser | User: {JsonConvert.SerializeObject(obj)}");
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _userService.AddUser(obj);
                    return StatusCode(StatusCodes.Status201Created, data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        #endregion


        #region update user api
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(UserDTO))]

        public async Task<IActionResult> UpdateUser(UserDTO obj)
        {
            Log.Information($"UpdateUser | User: {obj.Id}");
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _userService.Edit(obj);
                    if (data != null)
                    {
                        return Ok(data);
                    }
                    return BadRequest();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        #endregion


        #region single user api
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(UserDTO))]
        [SwaggerResponse(statusCode: StatusCodes.Status204NoContent)]

        public async Task<IActionResult> GetUserById(int id)
        {
            Log.Information($"GetUserById | ID: {id}");
            try
            {
                var data = await _userService.Get(id);
                if (data != null)
                {
                    return Ok(data);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region all users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(List<UserDTO>))]
        [SwaggerResponse(statusCode: StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                Log.Information("GetAllUsers");
                var data = await _userService.Get();
                if (data != null)
                {
                    return Ok(data);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region delete user api
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK)]

        public async Task<IActionResult> DeleteUser(int id)
        {
            Log.Information($"DeleteUser | ID: {id}");
            try
            {
                var data = await _userService.Delete(id);
                if (data)
                {
                    return NoContent();
                }
                return BadRequest("ID not found!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
