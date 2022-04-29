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
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
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
            }
            return RedirectToAction("index");
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("index");

        }
    }
}