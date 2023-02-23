using AppUser.BusinessServices.IServices;
using AppUser.BusinessServices.Services;
using AppUser.DataAccess.IRepositories;
using AppUser.DataAccess.Repositories;

namespace AppUser.API
{
    public static class CoreDependencies
    {
        public static void AppUserServices(this IServiceCollection services)
        {
            #region User
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

        }
    }
}
