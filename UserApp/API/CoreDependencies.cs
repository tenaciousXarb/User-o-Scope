using UserApp.BusinessServices.IServices;
using UserApp.BusinessServices.Services;
using UserApp.DataAccess.IRepositories;
using UserApp.DataAccess.Repositories;

namespace UserApp.API
{
    public static class CoreDependencies
    {
        public static void UserAppServices(this IServiceCollection services)
        {
            #region User
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

        }
    }
}
