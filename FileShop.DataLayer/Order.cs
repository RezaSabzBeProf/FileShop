using System.ComponentModel.DataAnnotations;

namespace AspCore_Course.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int OrderSum { get; set; }
        public bool IsFinaly { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }


        public virtual User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
