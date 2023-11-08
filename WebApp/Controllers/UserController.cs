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

        public IActionResult LoginRegistro() {

            return View();
        }

        #region Login
        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            Usuario u = s.Login(email, pass);

            return RedirectToAction("Index","Home");

        }
        #endregion

        #region Registrer

        [HttpPost]
        public IActionResult RegistrarMiembro(Miembro nuevo)
        {
            try
            {
                s.AddUsuario(nuevo);

                ViewBag.msg = "Felicidades, ya sos parte de nuestro universo 💫\t";
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
            }

            return View("LoginRegistro");
        }

        #endregion
    }

}
