using Dominio;
using Aplicacion;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;

namespace WebApp.Controllers
{
    public class PublicacionController : Controller
    {
        Sistema s = Sistema.GetInstancia();

        #region Environment
        private IWebHostEnvironment Environment;
        public PublicacionController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        #endregion

        public IActionResult Index()
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

            return View(PostsHablilitados(lid));
        }

        public IActionResult BuscarPublicaciones()
        {
            int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");

            if (idLogueado == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(PublicacionesHablilitadas(idLogueado));
        }
        [HttpPost]
        public IActionResult BuscarPublicaciones(string criterio, int VA)
        {
            int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");

            if (idLogueado == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (String.IsNullOrEmpty(criterio))
            {
                return View(PostsHablilitados(idLogueado));
            }
            else
            {
                List<Publicacion> pubsFiltradas = new List<Publicacion>();
                criterio = criterio.ToLower();
                foreach (Publicacion p in PublicacionesHablilitadas(idLogueado))
                {
                    string titulo = p.Titulo.ToLower();
                    string contenido = p.Contenido.ToLower();
                    if ((titulo.Contains(criterio) || contenido.Contains(criterio)) && p.CalcularVA() > VA)
                    {
                        pubsFiltradas.Add(p);
                    }
                }
                return View(pubsFiltradas);
            }
        }

        [HttpPost]
        public IActionResult ComentarPost(int idPost, string tituloComentario, string contenidoComentario, bool privacidadCheckbox)
        {
            int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");

            try
            {
                if (idLogueado != null)
                {
                    Miembro? m = s.MiembroById(idLogueado.Value);

                    if (!m.Bloqueado)
                    {
                        Privacidad priv = privacidadCheckbox ? Privacidad.privada : Privacidad.publica;

                        Post? p = s.GetPostById(idPost);

                        Comentario comentario = new Comentario(tituloComentario, contenidoComentario, priv, m);

                        s.AddComentario(comentario, p);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new Exception("Usuario bloqueado, no se puede realizar el comentario.");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }

            return RedirectToAction("Index");
        }


        public IActionResult DarLike(int id)
        {
            int? idl = HttpContext.Session.GetInt32("LogueadoId");
            try
            {
                Miembro? m = s.MiembroById(idl);
                if (idl != null && !m.Bloqueado)
                {
                    Reaccion r = new Reaccion(TipoReaccion.like, m);

                    Publicacion p = s.GetPublicacionById(id);

                    s.AddReaccion(p, r);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult DarDisLike(int id)
        {
            int? idl = HttpContext.Session.GetInt32("LogueadoId");
            try
            {
                Miembro? m = s.MiembroById(idl);
                if (idl != null && !m.Bloqueado)
                {
                    Reaccion r = new Reaccion(TipoReaccion.dislike, m);

                    Publicacion p = s.GetPublicacionById(id);

                    s.AddReaccion(p, r);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return RedirectToAction("Index");
        }

        #region Post
        public IActionResult Publicar()
        {
            int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
            if (idLogueado == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string? rol = HttpContext.Session.GetString("LogueadoRol");
            if (rol == "Admin")
            {
                return RedirectToAction("BuscarPublicaciones", "Publicacion");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Publicar(Post nuevo, IFormFile foto, Privacidad privacidad)
        {
            int? lid = HttpContext.Session.GetInt32("LogueadoId");
            try
            {
                if (foto == null)
                {
                    throw new Exception("La imagen no debe ser vacía");
                }

                if (nuevo == null)
                {
                    throw new Exception("El post no puede ser nulo");
                }

                SubirFoto(foto, lid);

                /* <------------ Renombro la imagen ------------>*/
                nuevo.Img = lid + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(foto.FileName);

                var autor = s.MiembroById(lid);

                if (autor.Bloqueado)
                {
                    throw new Exception($"El usuario {autor.Nombre} está actualmente bloqueado por un administrador y no puede realizar posts.");
                }

                nuevo.Autor = autor;
                nuevo.Privacidad = privacidad;

                s.AddPost(nuevo);

                return RedirectToAction("Index", "Publicacion");
            }
            catch (Exception e)
            {
                ViewBag.msg = e.Message;
                return View();
            }
        }

        public void SubirFoto(IFormFile foto, int? id)
        {
            /*long fileSize = foto.Length;*/

            string ext = Path.GetExtension(foto.FileName);

            string nombreSinExt = foto.Name;

            /* Establecer y crear ruta si no existe*/
            string ruta = Environment.WebRootPath + "//img//User//Posts//";

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            string fileName = id + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
            FileStream stream = new FileStream(ruta + fileName, FileMode.Create);

            foto.CopyTo(stream);

            stream.Close();

        }

        /* <------------ Posts a los que un usuario esta habilitado para ver ------------>*/
        public List<Post> PostsHablilitados(int? id)
        {
            int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");

            string? rol = HttpContext.Session.GetString("LogueadoRol");

            List<Post> Allposts = s.GetAllPosts(PublicacionesHablilitadas(id));

            if (rol == "Miembro")
            {
                List<Post> posts = new List<Post>();
                Miembro? Logueado = s.MiembroById(id);
                List<Miembro?> AmigosLogueado = s.GetAmigos(Logueado);

                foreach (Post post in Allposts)
                {
                    if (post.Privacidad == Privacidad.publica && !post.Censurada
                        || AmigosLogueado.Contains(post.Autor) 
                        || post.Autor.Id == idLogueado)
                    {
                        posts.Add(post);
                    }
                }
                return posts;
            }
            return Allposts;
        }

        /* <------------ Posts a los que un usuario esta habilitado para ver ------------>*/
        public List<Publicacion> PublicacionesHablilitadas(int? id)
        {
            string rol = HttpContext.Session.GetString("LogueadoRol");

            List<Publicacion> AllPubList = s.GetAllPublicaciones();
            if (rol == "Miembro")
            {
                List<Publicacion> pub = new List<Publicacion>();

                Miembro? Logueado = s.MiembroById(id);

                List<Miembro>? AmigosLogueado = s.GetAmigos(Logueado);

                foreach (Publicacion p in AllPubList)
                {
                    if (p.Privacidad == Privacidad.publica 
                        || AmigosLogueado.Contains(p.Autor) 
                        || p.Autor.Id == id)
                    {
                        pub.Add(p);
                    }
                }
                
                return pub;
            }
            return AllPubList;
        }

        #endregion

        #region Admins
        public IActionResult CensurarPost(int? id)
        {
            int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");

            string? rol = HttpContext.Session.GetString("LogueadoRol");

            if (idLogueado == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (rol == "Admin")
            {
                try
                {
                    s.CensurarPost(s.GetPostById(id));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return RedirectToAction("BuscarPublicaciones", "Publicacion");
        }
        public IActionResult DesCensurarPost(int? id)
        {
            int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
            string? rol = HttpContext.Session.GetString("LogueadoRol");

            if (idLogueado == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (rol == "Admin")
            {
                try { 
                    s.DesCensurarPost(s.GetPostById(id));
                } catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return RedirectToAction("BuscarPublicaciones", "Publicacion");
        }
        #endregion
    }
}
