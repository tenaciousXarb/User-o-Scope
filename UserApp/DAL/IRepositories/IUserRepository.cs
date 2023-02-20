using AppUser.DataAccess.AppData;

namespace AppUser.DataAccess.IRepositories
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        Task<List<User>> GetByPaginationAsync(int userPerPage, int pageNumber);
        Task<User> AuthenticateAsync(string email, string password);
        bool UniqueEmail(string email, int id);
    }
}
