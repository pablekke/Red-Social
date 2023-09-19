namespace Aplicacion.Publicaciones
{
    public class Sistema
    {
        #region Singleton
        private Sistema()
        {
            //Precarga();
        }
        private static Sistema instancia = null;
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
        public List<Miembro> GetAmigos(Miembro m)
        {
            return m.GetAmigos();
        }

        public List<Usuario> GetUsuarios()
        {
            return _usuarios;
        }

        public List<Invitacion> GetInvitaciones()
        {
            return _invitaciones;
        }

        public List<Publicacion> GetPublicaciones()
        {
            return _publicaciones;
        }

        #endregion

        #region Add Methods
        public void AddUsuario(Usuario u)
        {
            u.EsValido();
            _usuarios.Add(u);
        }
        public void AddPost(Post p)
        {
            p.EsValida();
            _publicaciones.Add(p);
        }
        public void AddPost(Post p)
        {
            p.EsValida();
            _publicaciones.Add(p);
        }
        public void AddInvitacion(Invitacion i)
        {

        }
        #endregion
    }
}
