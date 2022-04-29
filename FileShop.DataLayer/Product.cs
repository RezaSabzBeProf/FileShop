using FileShop.DataLayer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCore_Course.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ImageName { get; set; }

        public string ProductName { get; set; }

        public int ProductPrice { get; set; }

        public string FileLink { get; set; }

        public string Desc { get; set; }

        public List<Comment> ProductComments { get; set; }

        #region Relation
        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public ProductGroup ProductGroup { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        List<UserProduct> UserProducts { get; set; }
        #endregion
    }
}
