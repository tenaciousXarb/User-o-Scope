using UserApp.BusinessServices.DTO;

namespace UserApp.BusinessServices.IServices
{
    public interface IUserService
    {
        public Task<string?> AuthenticateUser(LoginDTO loginDTO);
        public Task<UserDTO?> AddUser(UserCreationDTO user);
        public Task<List<UserDTO>?> GetUsers();
        public Task<List<UserDTO>?> GetUsersWithPagination(int userPerPage, int pageNo);
        public Task<UserDTO?> GetUserById(int id);
        public Task<UserDTO?> EditUser(UserDTO user);
        public Task<bool> DeleteUser(int id);
    }
}
