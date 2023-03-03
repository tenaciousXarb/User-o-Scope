using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserApp.BusinessServices.DTO
{
    public class PageDetails
    {
        [DisplayName("Page number")]
        [Required]
        public int PageNo { get; set; }
        [DisplayName("Users per page")]
        [Required]
        public int UserPerPage { get; set; }
    }
}
