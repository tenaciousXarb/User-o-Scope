using AppUser.DataAccess.AppData;

namespace AppUser.DataAccess.IRepositories
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        public Task<List<User>?> GetAllSure();

        Task<List<User>> GetByPaginationAsync(int userPerPage, int pageNumber);
        Task<User?> AuthenticateAsync(string email, string password);
        Task<bool> UniqueEmail(string email, int id);
    }
}
