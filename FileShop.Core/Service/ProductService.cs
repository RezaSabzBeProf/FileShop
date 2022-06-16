using AspCore_Course.Models;
using FileShop.Core.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FileShop.Core.Service
{
    public class ProductService : IProductService
    {
        FarsLearnContext _context;
        public ProductService(FarsLearnContext context)
        {
            _context = context;
        }

        public void AddGroup(ProductGroup group)
        {
            _context.Add(group);
            _context.SaveChanges();
        }

        public void AddProduct(Product product, IFormFile productFile, IFormFile productImage)
        {
            product.ImageName = TopCoderZ.Core.Generator.NameGenerator.GenerateUniqCode() + Path.GetExtension(productImage.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Cover", product.ImageName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                productImage.CopyTo(stream);
            }

            product.FileLink = TopCoderZ.Core.Generator.NameGenerator.GenerateUniqCode() + Path.GetExtension(productFile.FileName);
            string fileLinkPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductFile", product.FileLink);
            using (var stream = new FileStream(fileLinkPath, FileMode.Create))
            {
                productImage.CopyTo(stream);
            }


            _context.Add(product);
            _context.SaveChanges();
        }

     
        public void AddToCard(int productId, string UserId)
        {
            var product = _context.Products.Find(productId);
            var user = _context.Users.Where(p=> p.UserName == UserId).SingleOrDefault();
            var order = _context.Orders.Where(p => p.IsFinaly == false && p.UserId == user.Id).SingleOrDefault();
            if(order == null)
            {
                _context.Orders.Add(new Order
                {
                    IsFinaly = false,
                    OrderSum = product.ProductPrice,
                    UserId = user.Id,
                    CreateDate = DateTime.Now,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail
                        {
                            Price = product.ProductPrice,
                            ProductId = product.Id
                        }
                    }
                });

            }
            else
            {
                var details = _context.OrderDetails.Include(p=> p.Product).SingleOrDefault(p => p.OrderId == order.OrderId && p.ProductId == product.Id);
                if(details != null)
                {

                }
                else
                {
                    details = new OrderDetail()
                    {
                        Price = product.ProductPrice,
                        ProductId = productId,
                        OrderId = order.OrderId
                    };
                    order.OrderDetails = new List<OrderDetail>();
                    order.OrderDetails.Add(details);
                    order.OrderSum += product.ProductPrice;

                }

            }
            _context.SaveChanges();
        }

        public List<SelectListItem> GetAllGroupForAddProduct()
        {
            var model = _context.ProductGroups.Select(g=>
                new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.GroupName
                }).ToList();
            return model;
        }

        public Product GetProductForShow(int id)
        {
            return _context.Products.Find(id);
        }

        public Order GetUserOrder(string userName)
        {
            return _context.Orders.Include(o=> o.User).Include(o=> o.OrderDetails).ThenInclude(o=> o.Product).Where(u => u.User.UserName == userName && u.IsFinaly == false).SingleOrDefault();
        }

        public List<ProductGroup> ListGroup()
        {
            return _context.ProductGroups.ToList();
        }

        public void IsFinalyTrue(Order order)
        {
            order.IsFinaly = true;
            foreach(var item in order.OrderDetails)
            {
                _context.UserProducts.Add(new DataLayer.UserProduct
                {
                    ProductId = item.ProductId,
                    UserId = order.UserId
                });
            }
            _context.SaveChanges();
        }

        public List<Product> UserProducts(string userName)
        {
            return _context.Products.Include(u => u.User).Where(p => p.User.UserName == userName).ToList();
        }

        public bool UserInProduct(string userName, int productId)
        {
            return _context.UserProducts.Include(u => u.User).Any(u => u.User.UserName == userName && u.ProductId == productId);
        }

        public List<Comment> GetProductComment(int id)
        {
            return _context.Comments.Include(u=> u.User).Where(p => p.ProductId == id).ToList();
        }

        public void CreateComment(Comment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
        }

        public List<Product> GetProductForIndex()
        {
            return _context.Products.ToList();
        }
    }
}
