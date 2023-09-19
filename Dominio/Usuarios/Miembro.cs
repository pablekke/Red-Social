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
        public bool SolicitudesBloqueadas { get; set; }
        private List<Miembro> _amigos = new List<Miembro>();
        #endregion

        public Miembro(string email, string pass, string nombre, string apellido, DateTime fnac) : base(email, pass)
        {
            Nombre = nombre;
            Apellido = apellido;
            fNac = fnac;
        }
        public Miembro() : base()
        {
           
        }
        #region Methods

        #region Lists
        public void AddAmigo(Miembro m)
        { 
            
        }
        public List<Miembro> GetAmigos()
        {
            return _amigos;
        }
        #endregion

        #region Validations

        public override void EsValido()
        {
            base.EsValido();
            if (String.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede ser vacío");
            }
            if (String.IsNullOrEmpty(Apellido))
            {
                throw new Exception("La apellido no puede ser vacía");
            }
            if ((DateTime.Now - fNac).TotalDays >= 365 * 120)
            {
                throw new Exception("La fecha de nacimiento es inválida");
            }
        }

        #endregion

        #endregion
    }
}
