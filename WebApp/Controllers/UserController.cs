using Aplicacion;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        Sistema s = Sistema.GetInstancia();

        public IActionResult Index()
        {
            return View();
        }

        #region Login
        public IActionResult Login(string email, string pass)
        {
            /*Usuario u = s.Login(email, pass);*/
            return View();
        }

        [HttpPost]
        public IActionResult LogIn()
        {
            return View();
        }
        #endregion

        #region Registrer
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Miembro nuevo)
        {

            return View();
        }
        #endregion
    }

}
