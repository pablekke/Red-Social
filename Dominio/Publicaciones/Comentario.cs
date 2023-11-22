using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Comentario:Publicacion, IValidacion
    {
        public Comentario(string titulo, string contenido, Privacidad priv, Miembro autor) :base(titulo, contenido, priv, autor) {
            
        }

        public Comentario() : base()
        {
        }


        public override void EsValido()
        {
            base.EsValido();
        }

        public override int CalcularVA()
        {
            return base.CalcularVA();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
