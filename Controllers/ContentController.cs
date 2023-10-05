using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace code_wizards_website.Controllers
{
    public class ContentController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.UserName = "Desconhecido";
            var nameClaim = User.FindFirst(ClaimTypes.Name);
            if(nameClaim != null)
            {
                ViewBag.UserName = nameClaim.Value;
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}