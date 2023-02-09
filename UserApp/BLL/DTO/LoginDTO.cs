using BLL.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class LoginDTO
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
