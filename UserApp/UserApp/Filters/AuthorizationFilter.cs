using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppUser.API.Filters
{
    /// <summary>
    /// AuthorizationFilter
    /// </summary>
    public class AuthorizationFilter : IAuthorizationFilter
    {
        /// <summary>
        /// OnAuthorization (AuthorizationFilter)
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers["Authorization"].ToString() == null || !context.HttpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer"))
            {
                throw new UnauthorizedAccessException("You're not authorized to access this page");
            }
        }
    }
}
