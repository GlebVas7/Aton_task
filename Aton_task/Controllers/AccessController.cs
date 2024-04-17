using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Aton_task.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Aton_task.Controllers
{
    public class AccessController : Controller
    {
        
        private readonly AtonContext _context;


        public AccessController( AtonContext context)
        {
            
            _context = context;
        }
        public IActionResult Login()
        {
            /*ClaimsPrincipal claimUser = HttpContext.User;

            if(claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Client","Home");*/

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string pass, bool keep)
        {
            var user = _context.Users.FirstOrDefault(u => (u.Login == login) && (u.Password == pass));
            if (user != null)
            {
                List<Claim> claim = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Login),
                    new Claim("OtherProperties","Example Role")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claim,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties() { 
                    AllowRefresh = true,
                    IsPersistent = user.KeepLoggetIn,
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                return RedirectToAction("Index", "Home", user);
            }

            ViewData["ValidateMessage"] = "Неверные логин или пароль!";
            return View();
        }
    }
}
