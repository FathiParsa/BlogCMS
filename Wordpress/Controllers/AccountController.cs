using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using DataAccessLayer.EF.Entities;
using DataAccessLayer.EF.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Wordpress.Models;

namespace Wordpress.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TryToLogin(LoginViewModel loginViewModel)
        {
            var uRepository = new UserRepository(new BloggingDbContext());
            var usr = await uRepository.FindByUsername(loginViewModel.Username);
            if (usr != null)
            {
                if (usr.Password == loginViewModel.Password)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim("Username", loginViewModel.Username),
                        new Claim("UserId", usr.Id.ToString())
                    };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, "Cookie");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        scheme: "mft",
                        principal: principal,
                        properties: new AuthenticationProperties
                        {
                            //IsPersistent = true, for remember me options
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                        });
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("ErrorInUsernameOrPassword", "Account");
        }

        public IActionResult ErrorInUsernameOrPassword()
        {
            return View();
        }
    }
}
