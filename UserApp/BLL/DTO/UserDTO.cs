using BLL.CustomValidator;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [UniqueEmail(ErrorMessage = "Email already exists!")]
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
    }
}
