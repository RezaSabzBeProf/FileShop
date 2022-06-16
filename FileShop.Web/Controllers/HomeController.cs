using AspCore_Course;
using AspCore_Course.Models;
using AspCore_Course.Models.DTOs;
using FileShop.Core.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace FileShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IUserService _userService;
        IProductService _productService;
        public HomeController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            var model = _productService.GetProductForIndex();
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (_userService.Login(model))
            {
                var usermodel = _userService.GetUserByUserName(model.UserName);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,usermodel.Id.ToString()),
                    new Claim(ClaimTypes.Name,usermodel.UserName.ToString()),
                    new Claim(ClaimTypes.Role,usermodel.Role.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var propertis = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMonths(1)
                };
                HttpContext.SignInAsync(principal, propertis);
                return RedirectToAction("index");

            }
            else
            {
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("index");

        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            user.CreateDate = DateTime.Now;
            user.Role = UserRole.NormalUser;
            string password = user.Password;
            password = Password_helper.EncodePassword(password);
            user.Password = password;
            _userService.AddUser(user);
            return Redirect("/home/Login");
        }
    }
}