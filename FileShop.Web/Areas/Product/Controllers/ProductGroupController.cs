using AspCore_Course.Models;
using FileShop.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FileShop.Web.Areas.Product.Controllers
{
    [Area("Product")]
    public class ProductGroupController : Controller
    {
        IProductService _productService;

        public ProductGroupController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var model = _productService.ListGroup();
            return View(model);
        }
        public IActionResult CreateGroup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateGroup(ProductGroup group)
        {
            _productService.AddGroup(group);
            return RedirectToAction("index");
        }
    }
}
