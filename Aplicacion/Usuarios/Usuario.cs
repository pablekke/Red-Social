using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public abstract class Usuario
    {
        #region Atributes
        private static int UltimoId {get;set;} = 1;
        public int Id { get;set;}
        public string Email { get; set; }
        public string Pass { get; set; }
        public bool EsAdmin { get; set; }
        public bool SolicitudesBloqueadas { get; set; }
        #endregion

        #region Methods
        public void ValidarCredenciales(string e, string p)
        {
            
        }

        #endregion

    }
}
