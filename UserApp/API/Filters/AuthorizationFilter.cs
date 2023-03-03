using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace UserApp.API.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        /* OnAuthorization (Authorization Filter: Executes before any other filters in the filter pipeline)
         * Pipeline:
         * (Entry) -> [Authorization Filter (OnAuthorization)] -> [Resource Filter (OnResourceExecuting)] -> [!!!!!! Model Binding & Validation !!!!!] -> [Action Filter (OnActionExecuting)] -> [!!!!!! Action Method !!!!!!] -> [Action Filter (OnActionExecuted)] -> [Exception Filter (OnException)] -> [Result Filter (OnResultExecuting)] -> [!!!!!! IActionResult execution !!!!!!] -> [Result FIlter (OnResultExecuted)] -> [Resource Filter (OnResourceExecuted)] -> [Exit]
         */
        /// <param name="context"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (token == null || !token.StartsWith("Bearer"))
            {
                throw new UnauthorizedAccessException("You're not authorized to access this page");
            }
            else
            {
                string tokenValue = token.Substring("Bearer ".Length).Trim();


                TokenValidationParameters validationParams = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = default(JwtSecurityToken);

                try
                {
                    jwtToken = tokenHandler.ReadJwtToken(tokenValue);
                    var claimsPrincipal = tokenHandler.ValidateToken(tokenValue, validationParams, out SecurityToken validatedToken);
                }
                catch (Exception ex)
                {
                    if (ex is SecurityTokenExpiredException)
                    {
                        throw new UnauthorizedAccessException($"Lifetime validation failed. Token expired at {jwtToken.ValidTo.AddHours(6)}, Current time: {DateTime.Now}.");
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(ex.Message);
                    }
                }
            }
        }
    }
}
