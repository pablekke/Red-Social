using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string user, string pass)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }

}
