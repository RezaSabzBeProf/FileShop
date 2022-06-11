using Dto.Payment;
using FileShop.Core.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZarinPal;
using ZarinPal.Class;

namespace FileShop.Web.Controllers
{
    public class FileController : Controller
    {
        IProductService _productService;
        public FileController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowProduct(int id)
        {
            var model = _productService.GetProductForShow(id);
            return View(model);
        }
        [Authorize]
        public IActionResult AddToCard(int id)
        {
            _productService.AddToCard(id, User.Identity.Name);
            return Redirect($"/file/showproduct/{id}");
        }
		[Authorize]
        public IActionResult ShowCard()
        {
            var model = _productService.GetUserOrder(User.Identity.Name);
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OnlinePayment()
        {
            var model = _productService.GetUserOrder(User.Identity.Name);
            var expose = new Expose();
            Payment _payment;
            Authority _authority;
            Transactions _transactions;
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            var result = await _payment.Request(new DtoRequest()
            {
                Mobile = "09111111111",
                CallbackUrl = "https://localhost:44342/VerifyPayment/" + model.OrderId,
                Description = $"پرداخت سبد خرید",
                Email = "test@gmail.com",
                Amount = model.OrderSum,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
            }, ZarinPal.Class.Payment.Mode.sandbox);
            //end new payment
            return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Authority);
        }
        [Route("/VerifyPayment/{id}")]
        public async Task<IActionResult> VerifyPayment(int id)
        {
            string authority = HttpContext.Request.Query["Authority"];
            var model = _productService.GetUserOrder(User.Identity.Name);
            var expose = new Expose();
            Payment _payment;
            Authority _authority;
            Transactions _transactions;
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            var result = await _payment.Verification(new DtoVerification() {
            Amount = model.OrderSum,
            Authority = authority,
            MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
            }, Payment.Mode.sandbox);
            if(result.Status == 100)
            {
                ViewBag.isPay = true;
                _productService.IsFinalyTrue(model);
            }
            else
            {
                ViewBag.IsPay = false;
            }
            return View();
        }
    }
}
