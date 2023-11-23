using Aplicacion;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Cryptography;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        Sistema s = Sistema.GetInstancia();

        private bool checkboxPrivacidad;

        /* public IActionResult Index()
        {
            return View();
        }*/

        #region Members

        #region People
        public IActionResult Personas()
        {
            int? lid = HttpContext.Session.GetInt32("LogueadoId");
            if (lid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var listaOrdenada = s.OrdenarPorNombreYApellido(s.BuscarMiembros("", lid));
            return View(listaOrdenada);
        }

        [HttpPost]
        public IActionResult Personas(string criterio)
        {
            int? lid = HttpContext.Session.GetInt32("LogueadoId");
            if (lid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (String.IsNullOrEmpty(criterio))
            {
                var listaSinCriterio = s.OrdenarPorNombreYApellido(s.BuscarMiembros("", lid));
                return View(listaSinCriterio);
            }
            var listaOrdenada = s.OrdenarPorNombreYApellido(s.BuscarMiembros(criterio, lid));
            return View(listaOrdenada);

        }



        public IActionResult SolicitudesPendientes()
        {
            int? lid = HttpContext.Session.GetInt32("LogueadoId");
            if (lid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string? rol = HttpContext.Session.GetString("LogueadoRol");
            if (rol == "Admin")
            {
                return RedirectToAction("BuscarPublicaciones", "Publicacion");
            }

            return View(s.GetSolicitudesPendientes(lid));
        }

        [HttpPost]
        public IActionResult AñadirAmigo(int id)
        {
            int? lid = HttpContext.Session.GetInt32("LogueadoId");
            try
            {
                if (!s.ExisteInvitaciónRechazada(lid, id))
                {
                    Invitacion? i = new Invitacion(s.MiembroById(lid), s.MiembroById(id));
                    s.EnviarInvitacion(i);
                }
                else
                {
                    s.GetSolicitudByIdDeMiembro(lid).Estado = Estado.pendiente;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Personas", "User");
        }

        [HttpPost]
        public IActionResult AceptarSolicitud(int id)
        {
            Invitacion? i = s.GetSolicitudById(id);
            try
            {
                s.AceptarInvitacion(i);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
            }
            return RedirectToAction("SolicitudesPendientes", "User");
        }

        [HttpPost]
        public IActionResult RechazarSolicitud(int id)
        {
            Invitacion? i = s.GetSolicitudById(id);
            try
            {
                s.DeclinarInvitacion(i);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("SolicitudesPendientes", "User");
        }
        #endregion

        #region Registrer
        public IActionResult Registro()
        {
            string? rol = HttpContext.Session.GetString("LogueadoRol");

            if (rol == "Miembro")
            {
                return RedirectToAction("Index", "Publicacion");
            }

            if (rol == "Admin")
            {
                return RedirectToAction("BuscarPublicaciones", "Publicacion");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Registro(Miembro nuevo)
        {
            try
            {
                s.AddUsuario(nuevo);

                Login(nuevo.Email, nuevo.Pass);
                return RedirectToAction("Index", "Publicacion");
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }

        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            string? rol = HttpContext.Session.GetString("LogueadoRol");

            if (rol == "Miembro")
            {
                return RedirectToAction("Index", "Publicacion");
            }

            if (rol == "Admin")
            {
                return RedirectToAction("BuscarPublicaciones", "Publicacion");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            Usuario? u = s.Login(email, pass);

            if (u != null)
            {
                HttpContext.Session.SetInt32("LogueadoId", u.Id);

                HttpContext.Session.SetString("LogueadoNombreApellido", u.Nombre + " " + u.Apellido);
                HttpContext.Session.SetString("LogueadoRol", u.GetType().Name);

                if (u is Miembro aux)
                {
                    HttpContext.Session.SetString("LogueadoFnac", aux.fNac.ToString());
                    return RedirectToAction("Index", "Publicacion");
                }
                else
                {
                    return RedirectToAction("BuscarPublicaciones", "Publicacion");
                }

            }
            else
            {
                ViewBag.msg = "Usuario y/o contraseña incorrectos";
                return View();
            }
        }

        #endregion

        #region Log Out
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #endregion

        #region Admins
        public IActionResult Bloquear(int? id)
        {
            string? rol = HttpContext.Session.GetString("LogueadoRol");
            if (rol == "Admin")
            {
                try
                {
                    Miembro? m = s.MiembroById(id);
                    s.Bloquear(m);
                } catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return RedirectToAction("Personas", "User");
        }
        public IActionResult DesBloquear(int? id)
        {
            string? rol = HttpContext.Session.GetString("LogueadoRol");
            if (rol == "Admin")
            {
                try
                {
                    Miembro? m = s.MiembroById(id);
                    s.DesBloquear(m);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return RedirectToAction("Personas", "User");
        }
        #endregion
    }

}