using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public abstract class Usuario: IValidacion
    {
        #region Atributes
        private static int _ultimoId { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public bool EsAdmin { get; set; }
        public bool Bloqueado { get; set; }
        #endregion

        #region Constructors

        public Usuario(string email, string pass, bool esAdmin)
        {
            Id = _ultimoId++;
            Email = email;
            Pass = pass;
            EsAdmin = esAdmin;
            Bloqueado = false;
        }

        public Usuario() { Id = _ultimoId++; }

        #endregion

        #region Methods
        public void ValidarCredenciales(string e, string p)
        {

        }
        public void Bloquear()
        {
            Bloqueado = true;
        }
        public void DesBloquear()
        {
            Bloqueado = false;
        }
        public virtual void EsValido()
        {
            if (String.IsNullOrEmpty(Email))
            {
                throw new Exception("Email vacío");
            }
            if (String.IsNullOrEmpty(Pass))
            {
                throw new Exception("Contraseña vacía");
            }
        }
        public virtual string ToString()
        {
            return $"Email: {Email}\nContraseña: {Pass}\nEsAdmin: {EsAdmin}";
        }
        #endregion

    }
}
