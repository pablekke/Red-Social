using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Post:Publicacion
    {
        #region Atributes
        public string Img { get; set; }
        public bool EsPublico { get; set; }
        public bool ComentariosCensurados { get; set; }
        private List<Comentario> _comentarios = new List<Comentario>();
        #endregion

        #region Methods
        public void AddComentario(Comentario c)
        {
            
        }

        public List<Comentario> GetComentarios()
        {
            return _comentarios;
        }
        #endregion
    }
}
