using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Post : Publicacion, IValidacion
    {
        #region Atributes

        public string Img { get; set; }

        public bool Censurada { get; set; }

        private List<Comentario> _comentarios = new List<Comentario>();

        #endregion

        #region Construcotrs

        public Post(string titulo, string contenido, string img, Privacidad priv, Miembro autor) : base(titulo, contenido, priv, autor)
        {
            Img = img;
            Censurada = false;
        }

        public Post() : base() { }

        #endregion

        #region Methods

        #region Lists
        public void AddComentario(Comentario c)
        {
            if (c is null)
            {
                throw new Exception("Comentario vacío");
            }

            if (c.Autor.EsAdmin)
            {
                throw new Exception("Los administradores no pueden realizar comentarios.");
            }

            _comentarios.Add(c);
        }

        public List<Comentario> GetComentarios()
        {
            return _comentarios;
        }

        #endregion

        #region Validations
        public override void EsValido()
        {
            base.EsValido();
            if (!Img.EndsWith(".jpg") && !Img.EndsWith(".png"))
            {
                throw new Exception("La imagen no posee un formato válido");
            }
        }

        #endregion

        public void Censurar()
        {
            Censurada = true;        
        }
        public void DesCensurar()
        {
            Censurada = false;
        }

        public override int CalcularVA()
        {
            int va = base.CalcularVA();

            if (Privacidad == Privacidad.publica)
            {
                va += 10;
            }

            return va;
        }

        #endregion
    }
}