using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Admin:Usuario, IValidacion
    {
        public Admin(string email, string pass, bool esAdmin) :base(email,pass,esAdmin) { }


        #region Methods
        public override void EsValido() 
        {
            base.EsValido();
        }
        #endregion
    }
}
