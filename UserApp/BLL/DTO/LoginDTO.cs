using System.ComponentModel.DataAnnotations;

namespace AppUser.BusinessServices.DTO
{
    public class LoginDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
