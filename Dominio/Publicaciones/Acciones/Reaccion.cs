using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Reaccion: IValidacion
    {
        #region Atributes

        public TipoReaccion TipoReaccion { get; set; }

        public Miembro Miembro { get; set; }

        #endregion

        #region Constructors
        public Reaccion(TipoReaccion reaccion, Miembro miembro)
        {
            TipoReaccion = reaccion;
            Miembro = miembro;
        }
        public Reaccion()
        {
            
        }
        #endregion
        public void EsValido()
        {
            if (Miembro is null)
            {
                throw new Exception("Miembro vacío");
                
            }
        }
        public override string ToString()
        {
            return $"Tipo de reaccion: {TipoReaccion}\nMiembro: {Miembro.Nombre} {Miembro.Apellido}";
        }
    }
}
