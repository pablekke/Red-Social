using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public abstract class Publicacion
    {
        #region Atributes
        private static int _ultimoId { get; set; } = 1;
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public Miembro Autor { get; set; }
        public DateTime fPublicado { get; set; }
        private List<Reaccion> _reacciones = new List<Reaccion>();
        #endregion

        #region Methods

        public virtual void EsValida()
        {
            if (Titulo.Length <= 2)
            {
                throw new Exception("El título debe tener más de 3 carácteres");
            }
            if (String.IsNullOrEmpty(Contenido))
            {
                throw new Exception("El contenido no debe ser vacío");
            }
        }

        public void AddReaccion(Reaccion r)
        {

        }
        public List<Reaccion> GetReacciones()
        {
            return _reacciones;
        }
        #endregion

    }
}
