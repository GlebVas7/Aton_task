using Aton_task.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;


namespace Aton_task.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AtonContext _context;
       


        public HomeController(ILogger<HomeController> logger, AtonContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            
            /*var user = HttpContext.User;*/
            var userId = HttpContext.Session.GetString("UserId");
            var user = _context.Users.Find(int.Parse(userId));
            var client = _context.Clients.ToList();
            if (user != null)
            {
                client = _context.Clients.Where(c => c.ResponsibleFullName == user.Login).ToList();
                return View(client);
            }
            else
            {
                return View(client);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        /*[AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Login(string username, string pass)
        {
            var user = _context.Users.FirstOrDefault(u => (u.Login == username) && (u.Password == pass));
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                return RedirectToAction("Client");
            }
            else
            {
                ViewBag.ErrorMessage = "Неверные логин или пароль!";
                return View();
            }
        }*/
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Access");
        }

        public IActionResult Client()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.Find(int.Parse(userId));
            var client = _context.Clients.Where(c => c.ResponsibleFullName == user.FIO).ToList();

            return View(client);
        }

        [HttpPost]
        public IActionResult Change(string clientId, string stat)
        {
            var client = _context.Clients.Find(clientId);
            client.Status = stat;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
