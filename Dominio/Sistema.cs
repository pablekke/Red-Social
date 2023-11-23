using Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        #region Singleton

        private Sistema()
        {
            Precarga();
        }

        private static Sistema? instancia = null;


        public static Sistema GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Sistema();
            }
            return instancia;
        }

        #endregion

        #region Lists

        private List<Usuario> _usuarios = new List<Usuario>();

        private List<Invitacion> _invitaciones = new List<Invitacion>();

        private List<Publicacion> _publicaciones = new List<Publicacion>();

        #endregion

        #region Get Methods

        public List<Miembro?> GetAmigos(Miembro? m)
        {
            return m.GetAmigos();
        }

        public List<Usuario> GetUsuarios()
        {
            return _usuarios;
        }

        public List<Miembro> GetMiembros()
        {
            List<Miembro> ret = new List<Miembro>();
            foreach (var u in _usuarios)
            {
                if (u is Miembro uu)
                {
                    ret.Add(uu);
                }
            }
            return ret;
        }

        public List<Post> GetAllPosts(List<Publicacion> listPub)
        {
            List<Post> posts = new List<Post>();

            foreach (Publicacion p in listPub)
            {
                if (p is Post)
                {
                    posts.Add((Post)p);
                }
            }

            return posts;
        }

        public List<Comentario> GetAllComentarios()
        {
            List<Comentario> comentarios = new List<Comentario>();
            foreach (Publicacion c in _publicaciones)
            {
                if (c is Comentario)
                {
                    comentarios.Add((Comentario)c);
                }
            }

            return comentarios;
        }

        public List<Publicacion> GetAllPublicaciones()
        {
            return _publicaciones;
        }

        public List<Invitacion> GetInvitaciones()
        {
            return _invitaciones;
        }
        public List<Invitacion> GetSolicitudesPendientes(int? id)
        {
            List<Invitacion> pends = new List<Invitacion>();
            Miembro? m = MiembroById(id);
            foreach (var i in _invitaciones)
            {
                if (i.Solicitado == m && i.Estado == Estado.pendiente)
                {
                    pends.Add(i);
                }
            }
            return pends;
        }
        public Invitacion? GetSolicitudById(int? idSolicitud)
        {
            Invitacion? inv = null;

            foreach (var i in _invitaciones)
            {
                if (i.Id == idSolicitud)
                {
                    inv = i;
                    break;
                }
            }
            return inv;
        }
        public Invitacion? GetSolicitudByIdDeMiembro(int? idMiembro)
        {
            Invitacion? inv = null;

            foreach (Invitacion i in _invitaciones)
            {
                if (i.Solicitante.Id == idMiembro || i.Solicitado.Id == idMiembro)
                {
                    inv = i;
                    break;
                }
            }
            return inv;
        }
        #endregion

        #region Add Methods

        public void AddUsuario(Usuario u)
        {
            if (u is null)
            {
                throw new Exception("Usuario vacío.");
            }
            u.EsValido();
            if (ExisteEmail(u.Email))
            {
                throw new Exception("Ese email ya fue registrado.");
            }
            _usuarios.Add(u);

        }


        public void AddPost(Post p)
        {
            if (p is null)
            {
                throw new Exception("Posts vacío.");
            }

            p.EsValido();

            if (p.Autor.EsAdmin)
            {
                throw new Exception("Los administradores no pueden realizar publicaciones.");
            }

            _publicaciones.Add(p);

        }

        public void AddComentario(Comentario c, Post p)
        {
            c.EsValido();

            if (p is null)
            {
                throw new Exception("Posts vacío.");
            }

            p.EsValido();

            if (p.Censurada)
            {
                throw new Exception("Publicacion censurada");
            }

            bool privado = p.Privacidad == Privacidad.privada;

            if (privado)
            {
                List<Miembro> _amigosAutorPost = p.Autor.GetAmigos();

                if (!_amigosAutorPost.Contains(c.Autor))
                {
                    throw new Exception("Solo los amigos pueden comentar.");
                }

                c.Privacidad = Privacidad.privada;
            }

            _publicaciones.Add(c);
            p.AddComentario(c);
        }

        public void AddInvitacion(Invitacion i)
        {
            if (i is null)
            {
                throw new Exception("Invitación vacía.");
            }

            i.EsValido();

            if (i.Solicitante.Bloqueado)
            {
                throw new Exception("Usuario bloqueado");
            }

            _invitaciones.Add(i);
        }

        public void AddReaccion(Publicacion p, Reaccion r)
        {
            if (p is null)
            {
                throw new Exception("Publicacion vacía");
            }

            p.EsValido();

            if (r is null)
            {
                throw new Exception("Reacción vacía");
            }

            r.EsValido();

            p.AddReaccion(r);
        }

        #endregion

        #region Login
        public Usuario? Login(string email, string pass)
        {
            Usuario? usuario = null;

            foreach (var u in _usuarios)
            {
                if (u.Email == email && u.Pass == pass)
                {
                    usuario = u;
                    break;
                }
            }

            return usuario;
        }

        public bool ExisteEmail(string email)
        {
            bool existe = false;
            foreach (var u in _usuarios)
            {
                if (u.Email == email)
                {
                    existe = true;
                    break;
                }
            }

            return existe;
        }

        #endregion

        #region Publications

        public Publicacion? GetPublicacionById(int publicacionId)
        {
            Publicacion? p = null;

            foreach (var publicacion in _publicaciones)
            {
                if (publicacion.Id == publicacionId)
                {
                    p = publicacion;
                    break;
                }
            }

            return p;
        }

        public Post? GetPostById(int? postId)
        {
            Post? p = null;

            foreach (var post in GetAllPosts(_publicaciones))
            {
                if (post.Id == postId)
                {
                    p = post;
                    break;
                }
            }

            return p;
        }

        public Comentario? GetCommentById(int commentId)
        {
            Comentario? c = null;

            foreach (var comment in GetAllComentarios())
            {
                if (comment.Id == commentId)
                {
                    c = comment;
                    break;
                }
            }

            return c;
        }

        public void EnviarInvitacion(Invitacion i)
        {
            if (i is null)
            {
                throw new Exception("Invitación vacía");
            }

            i.EsValido();

            _invitaciones.Add(i);
        }

        public void AceptarInvitacion(Invitacion i)
        {
            if (i is null)
            {
                throw new Exception("Invitación vacía");
            }

            i.EsValido();

            i.Aceptar();
        }

        public void DeclinarInvitacion(Invitacion i)
        {
            if (i is null)
            {
                throw new Exception("Invitación vacía");
            }

            i.EsValido();

            i.Declinar();
        }

        public void CensurarPost(Post? p)
        {
            if (p is null)
            {
                throw new Exception("Publicacion vacía");
            }

            p.EsValido();

            p.Censurar();
        }

        public void DesCensurarPost(Post p)
        {
            if (p is null)
            {
                throw new Exception("Publicacion vacía");
            }

            p.EsValido();

            p.DesCensurar();
        }

        public List<Publicacion> ListarPublicacionesPorEmail(string email)
        {
            List<Publicacion> ret = new List<Publicacion>();

            foreach (Publicacion p in _publicaciones)
            {
                if (p.Autor.Email == email)
                {
                    ret.Add(p);
                }
            }
            return ret;
        }

        public List<Publicacion> ListarPostComentadosPorEmail(string email)
        {
            List<Publicacion> ret = new List<Publicacion>();

            foreach (Post p in GetAllPosts(_publicaciones))
            {
                List<Comentario> coms = p.GetComentarios();

                foreach (Comentario c in coms)
                {
                    if (c.Autor.Email == email)
                    {
                        ret.Add(p);
                        break;
                    }
                }
            }

            return ret;
        }

        public List<Publicacion> ListarPublicacionesPorFecha(DateTime fInicio, DateTime fFin)
        {
            #region Excepcions
            double fFinDias = (DateTime.Today - fFin).TotalDays;
            //Se puso fFinDias < -1 ya que para la precarga se setea la fecha de hoy por lo
            //que siempre es 0, en un caso real se debería poner como 0.
            if (fFinDias < -1)
            {
                throw new Exception("Fecha de fin incorrecta");
            }
            if (fFin == fInicio)
            {
                throw new Exception("Las fechas no pueden ser iguales");
            }
            #endregion

            List<Publicacion> pub = new List<Publicacion>();

            foreach (Post p in GetAllPosts(_publicaciones))
            {
                if (p.fPublicado < fFin && p.fPublicado > fInicio)
                {
                    pub.Add(p);
                }
            }

            return pub;
        }

        public List<Usuario> ObtenerMiembrosConMasPublicaciones()
        {
            List<Usuario> ret = new List<Usuario>();

            int maxPublicaciones = 0;
            foreach (Usuario u in _usuarios)
            {
                int publicacionesDelMiembro = 0;

                foreach (Publicacion p in _publicaciones)
                {
                    if (p.Autor == u)
                    {
                        publicacionesDelMiembro++;
                    }
                }

                if (maxPublicaciones < publicacionesDelMiembro)
                {
                    ret.Clear();
                    ret.Add(u);
                    maxPublicaciones = publicacionesDelMiembro;
                }
                else if (publicacionesDelMiembro == maxPublicaciones)
                {
                    ret.Add(u);
                }
            }
            return ret;
        }

        public List<Publicacion> OrdenarPorTituloDesc(List<Publicacion> lista)
        {
            lista.Sort();
            return lista;
        }
        public List<Miembro> OrdenarPorNombreYApellido(List<Miembro?> lista)
        {
            lista.Sort();
            return lista;
        }
        #endregion

        #region Users

        public List<Miembro?> BuscarMiembros(string criterio, int? idLogueado)
        {
            List<Miembro?> ret = new List<Miembro?>();
            foreach (var m in GetMiembros())
            {
                string nombre = m.Nombre.ToLower();
                criterio = criterio.ToLower();
                if (nombre.Contains(criterio) && m.Id != idLogueado)
                {
                    ret.Add(m);
                }
            }
            return ret;
        }

        public Miembro? MiembroById(int? id)
        {
            Miembro? m = null;
            foreach (var miembro in GetMiembros())
            {
                if (miembro.Id == id)
                {
                    m = miembro;
                    break;
                }
            }
            return m;

        }

        public bool SonAmigos(int? id1, int? id2)
        {
            bool sonAmigos = false;

            Miembro? m1 = MiembroById(id1);
            Miembro? m2 = MiembroById(id2);

            if (m1.GetAmigos().Contains(m2))
            {
                sonAmigos = true;
            }
            return sonAmigos;
        }

        public bool ExisteInvitaciónPendiente(int? idSolicitante, int? idSolicitado)
        {
            bool existe = false;
            Miembro? m1 = MiembroById(idSolicitante);
            Miembro? m2 = MiembroById(idSolicitado);
            foreach (var i in _invitaciones)
            {
                if (
                    i.Solicitante.Id == idSolicitante && i.Solicitado.Id == idSolicitado &&
                    i.Estado == Estado.pendiente
                    )
                {

                    existe = true;
                    break;
                }
            }
            return existe;
        }

        public bool ExisteInvitaciónRechazada(int? id1, int? id2)
        {
            bool existe = false;
            Miembro? m1 = MiembroById(id1);
            Miembro? m2 = MiembroById(id2);
            foreach (var i in _invitaciones)
            {
                if (
                    (i.Solicitado.Id == id1 && i.Solicitante.Id == id2 ||
                     i.Solicitante.Id == id1 && i.Solicitado.Id == id2) &&
                     i.Estado == Estado.rechazada
                    )
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

        public void Bloquear(Miembro? u)
        {
            if (u is null)
            {
                throw new Exception("Miembro vacío.");
            }

            u.Bloquear();
        }

        public void DesBloquear(Miembro? u)
        {
            if (u is null)
            {
                throw new Exception("Miembro vacío.");
            }

            u.DesBloquear();
        }

        #endregion

        #region Precarga
        public void Precarga()
        {
            #region Miembros
            Miembro m1 = new Miembro("Pablo", "Suárez", "pablo.suarez@gmail.com", "pass1", false, new DateTime(2001, 06, 28));
            Miembro m2 = new Miembro("María", "García", "maria.garcia@gmail.com", "pass2", false, new DateTime(1985, 12, 10));
            Miembro m3 = new Miembro("José María", "López", "joseMa.lopez@gmail.com", "pass3", false, new DateTime(1980, 3, 25));
            Miembro m4 = new Miembro("Ana", "Rodríguez", "ana.rodriguez@gmail.com", "pass4", false, new DateTime(1995, 7, 2));
            Miembro m5 = new Miembro("Luis", "Martínez", "luis.martinez@gmail.com", "pass5", false, new DateTime(1988, 9, 20));
            Miembro m6 = new Miembro("Laura", "Sánchez", "laura.sanchez@gmail.com", "pass6", false, new DateTime(1979, 11, 30));
            Miembro m7 = new Miembro("Cristiano", "Ronaldo", "cristiano.ronaldo@gmail.com", "pass7", false, new DateTime(1992, 4, 18));
            Miembro m8 = new Miembro("Isabel", "Torres", "isabel.torres@gmail.com", "pass8", false, new DateTime(1983, 8, 12));
            Miembro m9 = new Miembro("Marín", "Gómez", "martin.gomez@gmail.com", "pass9", false, new DateTime(1987, 6, 5));
            Miembro m10 = new Miembro("Carmen", "Herrera", "carmen.herrera@gmail.com", "pass10", false, new DateTime(1998, 2, 15));
            Miembro m11 = new Miembro("Carla", "Gimenez", "carla.gimenez@gmail.com", "pass11", false, new DateTime(2000, 7, 15));

            AddUsuario(m1);
            AddUsuario(m2);
            AddUsuario(m3);
            AddUsuario(m4);
            AddUsuario(m5);
            AddUsuario(m6);
            AddUsuario(m7);
            AddUsuario(m8);
            AddUsuario(m9);
            AddUsuario(m10);
            AddUsuario(m11);

            //Admin
            Admin admin1 = new Admin("Gabriel", "Abalos", "gabriel.abalos@gmail.com", "admin1", true);
            Admin admin2 = new Admin("Antonella", "Martinez", "antonella.martinez@gmail.com", "admin2", true);

            AddUsuario(admin1);
            #endregion

            #region Invitaciones
            //Amigos
            Invitacion a11 = new Invitacion(m1, m2);
            Invitacion a12 = new Invitacion(m1, m3);
            Invitacion a13 = new Invitacion(m1, m4);
            Invitacion a14 = new Invitacion(m1, m5);
            Invitacion a15 = new Invitacion(m1, m6);
            Invitacion a16 = new Invitacion(m1, m7);
            Invitacion a17 = new Invitacion(m1, m8);
            Invitacion a18 = new Invitacion(m1, m9);
            Invitacion a19 = new Invitacion(m1, m10);

            Invitacion a21 = new Invitacion(m2, m3);
            Invitacion a22 = new Invitacion(m2, m4);
            Invitacion a23 = new Invitacion(m2, m5);
            Invitacion a24 = new Invitacion(m2, m6);
            Invitacion a25 = new Invitacion(m2, m7);
            Invitacion a26 = new Invitacion(m2, m8);
            Invitacion a27 = new Invitacion(m2, m9);


            EnviarInvitacion(a11);
            EnviarInvitacion(a12);
            EnviarInvitacion(a13);
            EnviarInvitacion(a14);
            EnviarInvitacion(a15);
            EnviarInvitacion(a16);
            EnviarInvitacion(a17);
            EnviarInvitacion(a18);
            EnviarInvitacion(a19);

            AceptarInvitacion(a11);
            AceptarInvitacion(a12);
            AceptarInvitacion(a13);
            AceptarInvitacion(a14);
            AceptarInvitacion(a15);
            AceptarInvitacion(a16);
            AceptarInvitacion(a17);
            AceptarInvitacion(a18);
            AceptarInvitacion(a19);

            EnviarInvitacion(a21);
            EnviarInvitacion(a22);
            EnviarInvitacion(a23);
            EnviarInvitacion(a24);
            EnviarInvitacion(a25);
            EnviarInvitacion(a26);
            EnviarInvitacion(a27);

            AceptarInvitacion(a21);
            AceptarInvitacion(a22);
            AceptarInvitacion(a23);
            AceptarInvitacion(a24);
            AceptarInvitacion(a25);
            AceptarInvitacion(a26);
            AceptarInvitacion(a27);

            //Pendientes
            Invitacion pend1 = new Invitacion(m3, m4);
            Invitacion pend2 = new Invitacion(m5, m6);
            Invitacion pend3 = new Invitacion(m11, m1);

            EnviarInvitacion(pend1);
            EnviarInvitacion(pend2);
            EnviarInvitacion(pend3);

            //Declinados
            Invitacion rechazada1 = new Invitacion(m7, m8);
            Invitacion rechazada2 = new Invitacion(m9, m10);

            EnviarInvitacion(rechazada1);
            EnviarInvitacion(rechazada2);

            DeclinarInvitacion(rechazada1);
            DeclinarInvitacion(rechazada2);
            #endregion

            #region Publicaciones
            Post post1 = new Post("Cocina", "10 mejores recetas.", "cocina.jpg", Privacidad.publica, m1);
            AddPost(post1);

            Post post2 = new Post("Viajes", "Mis viajes por todo el mundo.", "vacaciones.jpg", Privacidad.publica, m3);
            AddPost(post2);

            Post post3 = new Post("Música", "Descubriendo nuevos géneros musicales.", "musica.jpg", Privacidad.publica, m5);
            AddPost(post3);

            Post post4 = new Post("Deportes", "Mis logros deportivos y entrenamientos.", "deportes.jpg", Privacidad.publica, m7);
            AddPost(post4);

            Post post5 = new Post("Libros", "Mis reseñas de los últimos libros que he leído.", "libros.jpg", Privacidad.publica, m1);
            AddPost(post5);

            // Comentarios para el post1
            Comentario c1 = new Comentario("¡Me encanta esta receta!", "La probaré en casa.", Privacidad.publica, m2);
            Comentario c2 = new Comentario("Gracias por compartir", "Se ve deliciosa.", Privacidad.publica, m3);
            Comentario c3 = new Comentario("¡Excelente receta!", "Es mi nuevo plato favorito.", Privacidad.publica, m4);
            AddComentario(c1, post1);
            AddComentario(c2, post1);
            AddComentario(c3, post1);

            // Comentarios para el post2
            Comentario c4 = new Comentario("¡Qué hermoso lugar!", "¿Dónde fue tomada la foto?", Privacidad.publica, m1);
            Comentario c5 = new Comentario("Increíble vista", "Debe haber sido un viaje asombroso.", Privacidad.publica, m3);
            Comentario c6 = new Comentario("¡Me encantan tus viajes!", "Sigue explorando el mundo.", Privacidad.publica, m5);
            AddComentario(c4, post2);
            AddComentario(c5, post2);
            AddComentario(c6, post2);

            // Comentarios para el post3
            Comentario c7 = new Comentario("Buena selección musical", "También soy fan de ese género.", Privacidad.publica, m1);
            Comentario c8 = new Comentario("Me gustaría escuchar más", "¿Tienes alguna playlist recomendada?", Privacidad.publica, m2);
            Comentario c9 = new Comentario("Gran elección", "La música siempre es una buena compañía.", Privacidad.publica, m4);
            AddComentario(c7, post3);
            AddComentario(c8, post3);
            AddComentario(c9, post3);

            // Comentarios para el post4
            Comentario c10 = new Comentario("¡Eres un atleta impresionante!", "¿Cuál es tu deporte favorito?", Privacidad.publica, m1);
            Comentario c11 = new Comentario("Sigue así", "El esfuerzo siempre da sus frutos.", Privacidad.publica, m7);
            Comentario c12 = new Comentario("¡Vamos!", "¡Motivación para entrenar hoy!", Privacidad.publica, m10);
            AddComentario(c10, post4);
            AddComentario(c11, post4);
            AddComentario(c12, post4);

            // Comentarios para el post5
            Comentario c13 = new Comentario("Excelentes reseñas", "¿Tienes alguna recomendación de libros recientes?", Privacidad.publica, m2);
            Comentario c14 = new Comentario("Me encanta leer", "Siempre es bueno compartir opiniones sobre libros.", Privacidad.publica, m4);
            Comentario c15 = new Comentario("¡Gracias por las reseñas!", "Estoy buscando nuevas lecturas.", Privacidad.publica, m9);
            AddComentario(c13, post5);
            AddComentario(c14, post5);
            AddComentario(c15, post5);

            #region Reacciones

            Reaccion r1 = new Reaccion(TipoReaccion.like, m1);
            Reaccion r2 = new Reaccion(TipoReaccion.like, m3);
            Reaccion r3 = new Reaccion(TipoReaccion.dislike, m7);

            AddReaccion(post1, r1);
            AddReaccion(post3, r2);
            AddReaccion(post4, r3);
            AddReaccion(post2, r3);
            AddReaccion(post2, r2);

            AddReaccion(c1, r1);
            AddReaccion(c2, r2);
            AddReaccion(c4, r1);
            AddReaccion(c11, r3);
            AddReaccion(c11, r2);
            AddReaccion(c11, r1);

            #endregion
            #endregion

        }

        #endregion
    }
}