using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using code_wizards_website.Models;
using code_wizards_website.Data;
using Microsoft.AspNetCore.Authentication.Google;

namespace code_wizards_website.Controllers
{
    public class AccessController : Controller
    {
        private readonly CodeWizardsWebsiteDbContext _dbContext;

        public AccessController(CodeWizardsWebsiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal user = HttpContext.User;
            if(user.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Content");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountViewModel account)
        {
            
            AccountViewModel target = await _dbContext.Accounts
                .FirstOrDefaultAsync(x => x.Email == account.Email && x.Password == account.Password);
            
            if(target != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, target.Email),
                    new Claim(ClaimTypes.Name, target.Name)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties() {
                    AllowRefresh = true,
                    IsPersistent = target.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Content");
            }

            ViewData["ValidateMessage"] = "Email e/ou senha incorreto(s)";
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string name, string email, string password, string passwordSpell)
        {
            if(name != null && email != null && password != null  && passwordSpell != null)
            {
                if(password != passwordSpell)
                {
                    ViewData["PasswordsNotMatching"] = "As senhas não conferem.";
                    return View();
                }

                await _dbContext.Accounts.AddAsync(
                    new AccountViewModel
                    {
                        Name = name,
                        Email = email,
                        Password = password
                    }  
                );
                await _dbContext.SaveChangesAsync();
                ViewData["SuccessfulSignUp"] = "Cadastro realizado com sucesso!";

                return RedirectToAction("Login", "Access");
            }
            return View();
        }

        public async Task GoogleAuthentication()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties()
                {
                    RedirectUri = Url.Action("Login", "Access")
                }
            );
        }
    }
}