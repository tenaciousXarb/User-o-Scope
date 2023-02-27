using System.ComponentModel.DataAnnotations;

namespace UserApp.DataAccess.AppData
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
