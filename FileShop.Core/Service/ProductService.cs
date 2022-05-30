using AspCore_Course.Models;
using FileShop.Core.Service.Interface;
using Microsoft.AspNetCore.Http;
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

        public List<ProductGroup> ListGroup()
        {
            return _context.ProductGroups.ToList();
        }
    }
}
