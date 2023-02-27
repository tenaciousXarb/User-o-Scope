using UserApp.DataAccess.AppData;

namespace UserApp.DataAccess.IRepositories
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        Task<List<User>> GetByPaginationAsync(int userPerPage, int pageNumber);
        Task<User?> AuthenticateAsync(string email, string password);
        Task<bool> UniqueEmail(string email, int id);
    }
}
