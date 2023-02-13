using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AppUser.Controllers
{
    //[Authorize]
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
        /// <param name="userCreationDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(statusCode: StatusCodes.Status201Created, type: typeof(UserCreationDTO))]
        //[Authorize]
        public async Task<IActionResult> AddUser([FromBody, Required] UserCreationDTO userCreationDTO)
        {
            Log.Information($"AddUser (UserController) | User: {JsonConvert.SerializeObject(userCreationDTO)}");
            var response = await _userService.AddUser(userCreationDTO);

            return StatusCode(StatusCodes.Status201Created, response);
        }
        #endregion


        #region update user api
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userCreationDTO"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(UserCreationDTO))]
        //[Authorize]
        public async Task<IActionResult> UpdateUser(int id, UserCreationDTO userCreationDTO)
        {
            Log.Information($"UpdateUser (UserController) | User: {id}");
            var response = await _userService.Edit(id, userCreationDTO);
            
            return Ok(response);
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
        //[Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            Log.Information($"GetUserById (UserController) | ID: {id}");
            var response = await _userService.Get(id);
            
            return Ok(response);
        }
        #endregion


        #region all users api
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(List<UserDTO>))]
        [SwaggerResponse(statusCode: StatusCodes.Status204NoContent)]
        //[Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            Log.Information("GetAllUsers (UserController)");
            var response = await _userService.Get();

            return Ok(response);
        }
        #endregion


        #region all users with pagination api
        /// <summary>
        /// Get all users with pagination
        /// </summary>
        /// <param name="userPerPage"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        [HttpGet("pagination/{pageNo}")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(List<UserDTO>))]
        [SwaggerResponse(statusCode: StatusCodes.Status204NoContent)]
        //[Authorize]
        public async Task<IActionResult> GetAllUsersWithPagination([FromRoute] int pageNo, /*[FromBody]*/[FromQuery] int userPerPage = 2)
        {
            Log.Information("GetAllUsers (UserController)");
            var response = await _userService.GetAllPagination(userPerPage, pageNo);

            return Ok(response);
            /*try
            {
                var response = await _userService.GetAllPagination(userPerPage, pageNo);
                if (response.Count != 0)
                {
                    return Ok(response);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }*/
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
        //[Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            Log.Information($"DeleteUser (UserController) | ID: {id}");
            var response = await _userService.Delete(id);

            return Ok();
        }
        #endregion
    }
}