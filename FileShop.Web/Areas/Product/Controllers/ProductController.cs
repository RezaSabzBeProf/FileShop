using FileShop.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FileShop.Web.Areas.Product.Controllers
{
    [Area("Product")]
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult CreateProduct()
        {
            var model = _productService.GetAllGroupForAddProduct();
            ViewData["GroupList"] = new SelectList(model, "Value", "Text");
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(AspCore_Course.Models.Product product, IFormFile productFile, IFormFile productImage)
        {
            product.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _productService.AddProduct(product,productFile,productImage);
            return RedirectToAction("index");
        }
    }
}
