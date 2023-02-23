using AppUser.DataAccess.AppData;
using AppUser.DataAccess.IRepositories;

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
    }
}
