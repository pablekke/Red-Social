using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public abstract class Publicacion: IValidacion, IComparable<Publicacion>
    {
        #region Atributes

        private static int _ultimoId { get; set; }

        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Contenido { get; set; }

        public Miembro Autor { get; set; }

        public Privacidad Privacidad { get; set; }

        public DateTime fPublicado { get; set; }

        private List<Reaccion> _reacciones = new List<Reaccion>();

        #endregion

        #region Constructors
        public Publicacion(string titulo, string contenido, Privacidad priv, Miembro autor)
        {
            Id = _ultimoId++;
            Titulo = titulo;
            Contenido = contenido;
            Autor = autor;
            Privacidad = priv;
            fPublicado = DateTime.Now;
        }

        public Publicacion()
        {
            Id = _ultimoId++;
            fPublicado = DateTime.Now;
        }

        #endregion

        #region Methods
        public void AddReaccion(Reaccion reaccionNueva)
        {
            if (reaccionNueva is null) 
            {
                throw new Exception("Reaccion vacía");
            }

            bool reacciono = false;

            foreach (Reaccion reaccionHecha in _reacciones)
            {
                if (reaccionHecha.Miembro == reaccionNueva.Miembro)
                {
                    reaccionHecha.TipoReaccion = reaccionNueva.TipoReaccion;
                    
                    reacciono = true;
                    
                    break;
                }
            }

            if (!reacciono) { _reacciones.Add(reaccionNueva); }

        }

        public List<Reaccion> GetReacciones()
        {
            return _reacciones;
        }

        public virtual void EsValido()
        {
            if (Titulo.Length < 3)
            {
                throw new Exception("El título debe tener más de 3 carácteres");
            }

            if (String.IsNullOrEmpty(Contenido))
            {
                throw new Exception("El contenido no debe ser vacío");
            }

            if (Autor is null)
            {
                throw new Exception("El autor no debe ser vacío");
            }
        }

        public virtual int CalcularVA() 
        {
            int likes = 0;
            int dislikes = 0;

            foreach (Reaccion r in _reacciones)
            {
                if (r.TipoReaccion == TipoReaccion.like)
                {
                    likes++;
                }
                else 
                {
                    dislikes++;
                }
            }

            likes *= 5;
            dislikes *= -2;

            return likes + dislikes;
        }

        public virtual int CalcularCdadReacciones(TipoReaccion tp){ 
            int reaccion = 0;

            foreach (var r in _reacciones)
            {
                if (r.TipoReaccion == tp)
                {
                    reaccion++;
                }
            }
            return reaccion;
        }
        public int CompareTo(Publicacion? other)
        {
            int ret = 0;

            if (Titulo.CompareTo(other.Titulo) > 0)
            {
                ret = -1;
            }
            if (Titulo.CompareTo(other.Titulo) < 0)
            {
                ret = 1;
            }
            return ret;
        }

        public override string ToString()
        {
            return $"Título: {Titulo}\nContenido: {Contenido}\nPrivacidad: {Privacidad}\nAutor: {Autor.Nombre} {Autor.Apellido}";
        }
        #endregion

    }
}
