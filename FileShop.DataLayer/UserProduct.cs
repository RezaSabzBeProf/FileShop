using AspCore_Course.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShop.DataLayer
{
    public class UserProduct
    {
        [Key]
        public int UserProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }


        public Product Product { get; set; }
        public User User { get; set; }
    }
}
