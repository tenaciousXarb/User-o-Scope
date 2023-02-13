using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class UserCreationDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
    }
}
