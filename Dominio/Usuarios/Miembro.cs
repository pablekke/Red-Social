using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Miembro : Usuario, IValidacion
    {
        #region Atributes

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime fNac { get; set; }

        private List<Miembro> _amigos = new List<Miembro>();

        #endregion

        #region Constructors

        public Miembro(string nombre, string apellido, string email, string pass, bool esAdmin, DateTime fnac) : base(email, pass, esAdmin)
        {
            Nombre = nombre;
            Apellido = apellido;
            fNac = fnac;
        }

        public Miembro() : base() { }

        #endregion

        #region Methods

        #region Lists
        public void AddAmigo(Miembro m)
        {
            _amigos.Add(m);
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

            double EdadDias = (DateTime.Today - fNac).TotalDays;

            if (EdadDias >= 365 * 120 || EdadDias < 0)
            {
                throw new Exception("La fecha de nacimiento es inválida");
            }
        }
        public override string ToString()
        {
            return  base.ToString() + $"\nNombre: {Nombre}\nApellido: {Apellido}\nFecha de Nacimiento: {fNac}";
        }
        #endregion

        #endregion
    }
}
