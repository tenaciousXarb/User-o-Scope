using AppUser.BusinessServices.DTO;
using AppUser.BusinessServices.IServices;
using AppUser.DataAccess.AppData;
using AppUser.DataAccess.IRepositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppUser.BusinessServices.Services
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
            if (userCreationDTO != null)
            {
                if(userCreationDTO.Name == null || userCreationDTO.Name == string.Empty)
                {
                    throw new ArgumentException("Email can't be null or empty");
                }
                else if (userCreationDTO.Password == null || userCreationDTO.Password == string.Empty)
                {
                    throw new ArgumentException("Password can't be null or empty");
                }
                else if (userCreationDTO.Email == null || userCreationDTO.Email == string.Empty)
                {
                    throw new ArgumentException("Email can't be null or empty");
                }
                else if (userCreationDTO.Address == null || userCreationDTO.Address == string.Empty)
                {
                    throw new ArgumentException("Address can't be null or empty");
                }
                else
                {
                    if (_userRepo.UniqueEmail(email: userCreationDTO.Email, id: 0))
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
            }
            else
            {
                throw new NullReferenceException("Null user object");
            }
        }
        #endregion


        #region get all users
        public async Task<List<UserDTO>?> GetUsers()
        {
            var users = await _userRepo.GetAsync();

            return _mapper.Map<List<UserDTO>>(users);
        }
        #endregion


        #region get all with pagination
        public async Task<List<UserDTO>?> GetUsersPagination(int userPerPage, int pageNo)
        {
            if(userPerPage<0)
            {
                throw new ArgumentException("Users per page can't be less than zero!");
            }
            else if(pageNo<0)
            {
                throw new ArgumentException("Page number can't be less than zero!");
            }
            else
            {
                var users = await _userRepo.GetByPaginationAsync(userPerPage, pageNo);
                if (users.Count == 0)
                {
                    return null;
                }
                else
                {
                    return _mapper.Map<List<UserDTO>>(users);
                }
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
            if (userDTO != null)
            {
                if (userDTO.Id <= 0)
                {
                    throw new ArgumentException("ID can't be null and has to be a non-negative integer");
                }
                else if (userDTO.Name == null || userDTO.Name == string.Empty)
                {
                    throw new ArgumentException("Name can't be null or empty");
                }
                else if (userDTO.Password == null || userDTO.Password == string.Empty)
                {
                    throw new ArgumentException("Password can't be null or empty");
                }
                else if (userDTO.Email == null || userDTO.Email == string.Empty)
                {
                    throw new ArgumentException("Email can't be null or empty");
                }
                else if (userDTO.Address == null || userDTO.Address == string.Empty)
                {
                    throw new ArgumentException("Address can't be null or empty");
                }
                else
                {
                    var user = await _userRepo.GetByIdAsync(userDTO.Id);
                    if (user != null)
                    {
                        if (_userRepo.UniqueEmail(id: userDTO.Id, email: userDTO.Email))
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
            }
            else
            {
                throw new NullReferenceException("Null UserDTO object");
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
            if (loginDTO.Email == null || loginDTO.Email == string.Empty)
            {
                throw new ArgumentException("Email can't be null or empty!");
            }
            else if (loginDTO.Password == null || loginDTO.Password == string.Empty)
            {
                throw new ArgumentException("Password can't be null or empty!");
            }
            else
            {
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

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(90),
                        signingCredentials: creds);

                    var tk = new JwtSecurityTokenHandler().WriteToken(token);
                    return tk;
                }
                else
                {
                    throw new ArgumentException("Invalid username or password");
                }
            }
        }
        #endregion
    }
}
