using Microsoft.AspNetCore.Mvc.Filters;

namespace AppUser.API.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        /* OnAuthorization (Authorization Filter: Executes before any other filters in the filter pipeline)
         * Pipeline:
         * (Entry) -> [Authorization Filter (OnAuthorization)] -> [Resource Filter (OnResourceExecuting)] -> [!!!!!! Model Binding & Validation !!!!!] -> [Action Filter (OnActionExecuting)] -> [!!!!!! Action Method !!!!!!] -> [Action Filter (OnActionExecuted)] -> [Exception Filter (OnException)] -> [Result Filter (OnResultExecuting)] -> [!!!!!! IActionResult execution !!!!!!] -> [Result FIlter (OnResultExecuted)] -> [Resource Filter (OnResourceExecuted)] -> [Exit]
         */

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
