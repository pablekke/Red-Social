using Dominio;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class Invitacion:IValidacion
    {
        #region Atributes

        private static int _ultimoId { get; set; }

        public int Id { get; set; }

        public Miembro Solicitante { get; set; }

        public Miembro Solicitado { get; set; }

        public Estado Estado { get; set; }

        public DateTime fSolicitud { get; set; }

        #endregion

        #region Constructors
        public Invitacion(Miembro solicitante, Miembro solicitado)
        {
            Id = _ultimoId++;
            Solicitante = solicitante;
            Solicitado = solicitado;
            Estado = Estado.pendiente;
            fSolicitud = DateTime.Now;
        }

        public Invitacion() { Id = _ultimoId++; }
        #endregion

        #region Methods
        public void Aceptar()
        {
            Estado = Estado.aprobada;
            Solicitante.AddAmigo(Solicitado);
            Solicitado.AddAmigo(Solicitante);
        }

        public void Declinar()
        {
            Estado = Estado.rechazada;
        }

        public void EsValido() 
        {
            if (Solicitante.Bloqueado)
            {
                throw new Exception("Solicitante bloqueado");
            }

            if ( Solicitante == null ) 
            {
                throw new Exception("El solicitante no debe ser vacío");
            }

            if (Solicitado == null)
            {
                throw new Exception("El solicitado no debe ser vacío");
            }

            if (Solicitado == Solicitante)
            {
                throw new Exception("Miembros iguales");
            }
        }

        public override string ToString()
        {
            return $"Solicitante:{Solicitante.Nombre} {Solicitante.Apellido}\nSolicitado: {Solicitado.Nombre} {Solicitado.Apellido}";
        }

        #endregion
    }
}
