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
        private static int _ultimoId { get; set; } = 1;
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public bool EsAdmin { get; set; }
        #endregion

        public Usuario(string email, string pass)
        {
            Id = _ultimoId++;
            Email = email;
            Pass = pass;
        }

        public Usuario() { Id = _ultimoId++; }
        #region Methods
        public void ValidarCredenciales(string e, string p)
        {

        }
        public virtual void EsValido() 
        {
            if (String.IsNullOrEmpty(Email))
            {
                throw new Exception("El email no puede ser vacío");
            }
            if (String.IsNullOrEmpty(Pass)) 
            {
                throw new Exception("La contraseña no puede ser vacía");
            }
        }
        #endregion

    }
}
