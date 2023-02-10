using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService: IAuthService
    {
        private readonly IConfiguration _configuration;
        
        public AuthService()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt");
        }

        public async Task<string?> AuthenticateUser(string uname, string pass)
        {
            var user = await DataAccessFactory.UserAuthDataAccess().Authenticate(uname, pass);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("uid", user.Id.ToString()),
                    new Claim("user", user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Key"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Issuer"],
                    _configuration["Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(90),
                    signingCredentials: creds);

                var tk = new JwtSecurityTokenHandler().WriteToken(token);
                return tk;
            }
            throw new UnauthorizedAccessException("Invalid username or password");
        }
    }
}
