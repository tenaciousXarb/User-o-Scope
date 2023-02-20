using System.ComponentModel.DataAnnotations;

namespace AppUser.BusinessServices.DTO
{
    public class PageDetails
    {
        [Required]
        public int PageNo { get; set; }
        [Required]
        public int UserPerPage { get; set; }
    }
}
