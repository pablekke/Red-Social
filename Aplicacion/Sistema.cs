using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Usuario> GetAmigos(Usuario u)
        {
            u.
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
        public void AddUsuario()
        {

        }
        public void AddPublicacion()
        {

        }
        public void AddInvitacion()
        {

        }
        #endregion
    }
}
