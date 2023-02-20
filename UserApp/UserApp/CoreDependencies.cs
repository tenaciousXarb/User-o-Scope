using AppUser.BusinessServices.IServices;
using AppUser.BusinessServices.Services;
using AppUser.DataAccess.IRepositories;
using AppUser.DataAccess.Repositories;

namespace AppUser.API
{
    /// <summary>
    /// CoreDependencies
    /// </summary>
    public static class CoreDependencies
    {
        /// <summary>
        /// AppUserServices
        /// </summary>
        /// <param name="services"></param>
        public static void AppUserServices(this IServiceCollection services)
        {
            #region User
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
