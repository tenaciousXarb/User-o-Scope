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
        [SwaggerResponse(statusCode: StatusCodes.Status201Created, type: typeof(UserCreationDTO))]
        //[Authorize]
        public async Task<IActionResult> AddUser(UserCreationDTO obj)
        {
            Log.Information($"AddUser (UserController) | User: {JsonConvert.SerializeObject(obj)}");
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
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(UserCreationDTO))]
        //[Authorize]
        public async Task<IActionResult> UpdateUser(int id, UserCreationDTO obj)
        {
            Log.Information($"UpdateUser (UserController) | User: {id}");
            if (ModelState.IsValid)
            {
                try
                {
                    var data = await _userService.Edit(id, obj);
                    return Ok(data);
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
        //[Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            Log.Information($"GetUserById (UserController) | ID: {id}");
            try
            {
                var data = await _userService.Get(id);
                if(data != null)
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
            try
            {
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
            try
            {
                Log.Information("GetAllUsers (UserController)");
                var data = await _userService.GetAllPagination(userPerPage, pageNo);
                if (data.Count != 0)
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
        //[Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            Log.Information($"DeleteUser (UserController) | ID: {id}");
            try
            {
                var data = await _userService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}