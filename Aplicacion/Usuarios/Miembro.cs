using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Miembro:Usuario
    {
        #region Atributes
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime fNac { get; set; }
        private List<Miembro> _amigos = new List<Miembro>();
        #endregion

        #region Methods

        public void AddAmigo(Miembro m)
        { 
            
        }
        public List<Miembro> GetAmigos()
        {
            return _amigos;
        }            
        #endregion
    }
}
