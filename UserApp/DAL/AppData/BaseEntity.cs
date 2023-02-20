using System.ComponentModel.DataAnnotations;

namespace AppUser.DataAccess.AppData
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
