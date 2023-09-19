using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Invitacion
    {
        #region Atributes
        private static int _ultimoId { get; set; } = 1;
        public int Id { get; set; }
        public Miembro Solicitante { get; set; }
        public Miembro Solicitado { get; set; }
        public Estado Estado { get; set; }
        public DateTime fSolicitud { get; set; }
        #endregion

        #region Methods
        public void Aceptar()
        {

        }
        public void Declinar()
        {

        }
        #endregion
    }
}
