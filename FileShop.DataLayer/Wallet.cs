using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCore_Course.Models
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        public int UserId { get; set; }

        public int Amount { get; set; }

        public bool IsPay { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
