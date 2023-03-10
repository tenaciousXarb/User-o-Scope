using Dapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Data;
using UserApp.API.Filters;
using UserApp.BusinessServices.DTO;
using UserApp.BusinessServices.IServices;

namespace UserApp.API.Controllers
{
    //[Authorize]
    [AuthorizationFilter]
    public class UserController : BaseApiController
    {
        #region fields
        private readonly IUserService _userService;
        private readonly IDbConnection _connection;
        #endregion


        #region ctor
        public UserController(IUserService userService, IDbConnection connection)
        {
            _userService = userService;
            _connection = connection;
        }
        #endregion


        #region add user api
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="userCreationDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(statusCode: StatusCodes.Status201Created, type: typeof(UserDTO))]
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
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(UserDTO))]
        //[Authorize]
        public async Task<IActionResult> UpdateUser([FromBody, Required] UserDTO userDTO)
        {
            Log.Information($"UpdateUser (UserController) | User: {userDTO.Id}");
            var response = await _userService.EditUser(userDTO);

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
            var response = await _userService.GetUserById(id);

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
            var response = await _userService.GetUsers();

            return Ok(response);
        }
        #endregion


        #region all users with pagination api
        /// <summary>
        /// Get all users with pagination
        /// </summary>
        /// <param name="pageDetails"></param>
        /// <returns></returns>
        [HttpGet("pagination")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(List<UserDTO>))]
        [SwaggerResponse(statusCode: StatusCodes.Status204NoContent)]
        //[Authorize]
        public async Task<IActionResult> GetAllUsersWithPagination([FromQuery] PageDetails pageDetails)
        {
            Log.Information("GetAllUsersWithPagination (UserController)");
            var response = await _userService.GetUsersWithPagination(pageDetails);

            return Ok(response);
        }
        #endregion


        #region delete user api
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK)]
        //[Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            Log.Information($"DeleteUser (UserController) | ID: {id}");
            var response = await _userService.DeleteUser(id);

            return Ok();
        }
        #endregion


        #region search user api
        /// <summary>
        /// Search user by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(List<UserDTO>))]
        [SwaggerResponse(statusCode: StatusCodes.Status204NoContent)]
        //[Authorize]
        public async Task<IActionResult> SearchUser([FromQuery, Required] string name)
        {
            Log.Information($"SearchUser (UserController) | Search string: {name}");

            #region Inline Table-Valued Function
            //Stored Inline Table-Valued Function
            var response = (await _connection.QueryAsync<UserDTO>($"select * from search_user_inline(@Name)", param: new { Name = name })).AsList();
            #endregion

            #region Procedure
            //Stored Procedure
            /*var response = (await _connection.QueryAsync<UserDTO>(sql: "SearchUser", param: new { Name = name }, commandType: CommandType.StoredProcedure)).AsList();*/
            #endregion

            return response.Count != 0 ? Ok(response) : NoContent();
        }
        #endregion
    }
}