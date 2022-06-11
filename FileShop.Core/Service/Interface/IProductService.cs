using AspCore_Course.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FileShop.Core.Service.Interface
{
    public interface IProductService
    {
        void AddGroup(ProductGroup group);

        List<ProductGroup> ListGroup();

        List<SelectListItem> GetAllGroupForAddProduct();

        void AddProduct(Product product, IFormFile productFile, IFormFile productImage);

        Product GetProductForShow(int id);

        void AddToCard(int productId,string UserId);

        Order GetUserOrder(string userName);

        void IsFinalyTrue(Order order);
    }
}
