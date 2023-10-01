using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Reaccion
    {
        #region Atributes

        public TipoReaccion TipoReaccion { get; set; }

        public Miembro Miembro { get; set; }

        #endregion

        #region Constructor
        public Reaccion(TipoReaccion reaccion, Miembro miembro)
        {
            TipoReaccion = reaccion;
            Miembro = miembro;
        }
        public Reaccion()
        {
            
        }

        #endregion

    }
}
