using System.ComponentModel.DataAnnotations;

namespace AspCore_Course.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [MaxLength(400)]
        public string CommentText { get; set; }
        public DateTime CreateDate { get; set; }


        public Product Product { get; set; }
        public User User { get; set; }
    }
}
