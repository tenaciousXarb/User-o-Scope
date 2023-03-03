using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApp.BusinessServices.DTO;
using UserApp.BusinessServices.IServices;
using UserApp.BusinessServices.Validator;
using UserApp.DataAccess.AppData;
using UserApp.DataAccess.IRepositories;

namespace UserApp.BusinessServices.Services
{
    public class UserService : IUserService
    {
        #region fields
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;
        #endregion


        #region ctor
        public UserService(IMapper mapper, IUserRepository userRepo, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _configuration = configuration;
        }
        #endregion


        #region add user
        public async Task<UserDTO?> AddUser(UserCreationDTO userCreationDTO)
        {
            ObjectValidator.CheckNullOrEmpty(userCreationDTO);

            if (await _userRepo.UniqueEmail(email: userCreationDTO.Email, id: 0))
            {
                var userToCreate = _mapper.Map<User>(userCreationDTO);
                var createdUser = await _userRepo.AddAsync(userToCreate);
                if (createdUser != null)
                {
                    return _mapper.Map<UserDTO>(createdUser);
                }
                else
                {
                    throw new InvalidOperationException("Unable to add user");
                }
            }
            else
            {
                throw new ArgumentException("Email already exists");
            }
        }
        #endregion


        #region get all users
        public async Task<List<UserDTO>?> GetUsers()
        {
            //var users = await _userRepo.GetAsync();
            var users = await _userRepo.GetAsync();

            return _mapper.Map<List<UserDTO>>(users);
        }
        #endregion


        #region get all with pagination
        public async Task<List<UserDTO>?> GetUsersWithPagination(PageDetails pageDetails)
        {
            ObjectValidator.CheckNullOrEmpty(pageDetails);

            var users = await _userRepo.GetByPaginationAsync(pageNumber: pageDetails.PageNo, userPerPage: pageDetails.UserPerPage);
            if (users.Count == 0)
            {
                return null;
            }
            else
            {
                return _mapper.Map<List<UserDTO>>(users);
            }
        }
        #endregion


        #region get user by ID
        public async Task<UserDTO?> GetUserById(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            return _mapper.Map<UserDTO>(user);
        }
        #endregion


        #region update user
        public async Task<UserDTO?> EditUser(UserDTO userDTO)
        {
            ObjectValidator.CheckNullOrEmpty(userDTO);

            var user = await _userRepo.GetByIdAsync(userDTO.Id);
            if (user != null)
            {
                if (await _userRepo.UniqueEmail(id: userDTO.Id, email: userDTO.Email))
                {
                    var userToUpdate = _mapper.Map<User>(userDTO);
                    var updatedUser = await _userRepo.UpdateAsync(userToUpdate);
                    if (updatedUser != null)
                    {
                        return _mapper.Map<UserDTO>(updatedUser);
                    }
                    else
                    {
                        throw new InvalidOperationException("Values not updated");
                    }
                }
                else
                {
                    throw new ArgumentException("Email not unique");
                }
            }
            else
            {
                throw new NullReferenceException("User not found");
            }
        }
        #endregion


        #region delete user
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user != null)
            {
                var userDeleted = await _userRepo.DeleteAsync(id);
                if (userDeleted)
                {
                    return userDeleted;
                }
                else
                {
                    throw new InvalidOperationException("Unable to delete user");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        #endregion


        #region authenticate user
        public async Task<string?> AuthenticateUser(LoginDTO loginDTO)
        {
            ObjectValidator.CheckNullOrEmpty(loginDTO);

            var user = await _userRepo.AuthenticateAsync(email: loginDTO.Email, password: loginDTO.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("uid", user.Id.ToString()),
                        new Claim("user", user.Email)
                    };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(90),
                    signingCredentials: signingCredentials);

                var writtenToken = new JwtSecurityTokenHandler().WriteToken(token);
                return writtenToken;
            }
            else
            {
                throw new ArgumentException("Invalid username or password");
            }
        }
        #endregion
    }
}
