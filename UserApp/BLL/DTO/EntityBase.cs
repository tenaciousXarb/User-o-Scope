using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
