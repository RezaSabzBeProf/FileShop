using System.ComponentModel.DataAnnotations;

namespace AspCore_Course.Models
{
    public class ProductGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GroupName { get; set; }
    }
}
