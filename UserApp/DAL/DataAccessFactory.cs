using DAL.EF;
using DAL.Interfaces;
using DAL.Repo;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<User, int, User> UserDataAccess()
        {
            return new UserRepo();
        }
        public static IAuth<User> UserAuthDataAccess()
        {
            return new UserRepo();
        }
    }
}