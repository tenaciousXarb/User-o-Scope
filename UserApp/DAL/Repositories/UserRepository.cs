using AppUser.DataAccess.AppData;
using AppUser.DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AppUser.DataAccess.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        #region fields
        private readonly UserProjectDbContext _context;
        #endregion


        #region ctor
        public UserRepository(UserProjectDbContext context) : base(context)
        {
            _context = context;
        }
        #endregion


        #region get by pagination async (DB)
        public async Task<List<User>> GetByPaginationAsync(int userPerPage, int pageNumber)
        {
            return await _context.Users
                .OrderBy(user => user.Id)
                .Skip(userPerPage * (pageNumber - 1))
                .Take(userPerPage)
                .ToListAsync();
        }
        #endregion


        #region authenticate async (DB)
        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
        #endregion


        #region validate email (DB)
        public bool UniqueEmail(string email, int id = 0)
        {
            var users = Task.Run(() => GetAsync()).Result;
            return !(users.Any(x => x.Email == email && x.Id != id));
        }
        #endregion
    }
}
