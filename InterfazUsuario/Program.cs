using Aplicacion;
using Dominio;

namespace InterfazUsuario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sistema S = Sistema.GetInstancia();
            DateTime datetimeValido = new DateTime(2001, 06, 28);
            bool ejecutandose = true;
            while (ejecutandose)
            {
                Console.Clear();
                Console.WriteLine("Menú:");
                Console.WriteLine("1. Alta de miembro.");
                Console.WriteLine("2. Listar todas las publicaciones que ha realizado un miembro por email.");
                Console.WriteLine("3. Listar los posts en los que haya realizado comentarios.");
                Console.WriteLine("4. Dadas dos fechas, se listarán los posts realizados entre esas fechas inclusive\n   El listado estará ordenado por título en forma descendente.");
                Console.WriteLine("5. Obtener los miembros que hayan realizado más publicaciones de cualquier tipo.");
                Console.WriteLine("6. Salir");

                Console.Write("Ingrese la opción deseada: ");
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":

                        Console.Write("Ingrese el nombre del nuevo miembro: ");
                        string nombre = Console.ReadLine();

                        Console.Write("Ingrese el apellido del nuevo miembro: ");
                        string apellido = Console.ReadLine();

                        bool fechaValida = false;
                        DateTime fechaDT = DateTime.Now;
                        while (!fechaValida)
                        {
                            Console.Write("Ingrese la fecha de nacimiento del nuevo miembro (yyyy-MM-dd): ");
                            string fNac = Console.ReadLine();

                            try
                            {
                                fechaDT = DateTime.Parse(fNac);
                                fechaValida = true;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Fecha incorrecta. Aseguráte de que la fecha esté en el formato yyyy-MM-dd.");
                            }
                        }

                        Console.Write("Ingrese el email del nuevo miembro: ");
                        string email = Console.ReadLine();

                        Console.Write("Ingrese la contraseña del nuevo miembro: ");
                        string pass = Console.ReadLine();
                        try
                        {

                            Miembro nuevoMiembro = new Miembro(nombre, apellido, email, pass, false, datetimeValido); /*fechaDT);*/

                            S.AddUsuario(nuevoMiembro);

                            Console.WriteLine("Miembro dado de alta con éxito.");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "2":

                        Console.Write("Ingrese el email del miembro: ");
                        string emailMiembro = Console.ReadLine();

                        List<Publicacion> publicaciones = S.ListarPublicacionesPorEmail(emailMiembro);

                        if (publicaciones.Count == 0)
                        {
                            Console.WriteLine("El usuario no realizó ninguna publicación");
                        }
                        else
                        {
                            Console.WriteLine($"Publicaciones realizadas por {emailMiembro}:");
                            foreach (Publicacion p in publicaciones)
                            {
                                if (p is Post)
                                {
                                    Console.WriteLine($"Tipo: Post - Contenido: {p.Contenido}");
                                }
                                else if (p is Comentario)
                                {
                                    Console.WriteLine($"Tipo: Comentario - Contenido: {p.Contenido}");
                                }
                            }
                        }
                        break;

                    case "3":

                        Console.Write("Ingrese el email del miembro: ");
                        string emailMiembro3 = Console.ReadLine();

                        List<Publicacion> postComentados = S.ListarPostComentadosPorEmail(emailMiembro3);

                        if (postComentados.Count == 0)
                        {
                            Console.WriteLine("El usuario no realizó ninguna publicación");
                        }
                        else
                        {
                            Console.WriteLine($"Comentarios realizadas por {emailMiembro3}:");
                            foreach (Publicacion c in postComentados)
                            {
                                Console.WriteLine($"Tipo: Comentario - Contenido: {c.Contenido}");
                            }
                        }
                        break;

                    case "4":
                        Console.WriteLine("Ingresá fecha de inicio (yyyy-MM-dd): ");
                        DateTime fInicio = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Ingresá fecha de fin (yyyy-MM-dd): ");
                        DateTime fFin = DateTime.Parse(Console.ReadLine());

                        try
                        {
                            List<Publicacion> pub = S.ListarPublicacionesPorFecha(fInicio, fFin);

                            if (pub.Count == 0)
                            {
                                Console.WriteLine("No hay publicaciones del usuario en el período dado");
                            }
                            else {
                                pub = S.OrdenarPorTituloDesc(pub);

                                foreach (Publicacion p in pub)
                                {
                                    Console.WriteLine
                                    (
                                        $"Id: {p.Id}\n" +
                                        $"Fecha de publicación: {p.fPublicado}\n" +
                                        $"Título: {p.Titulo}\n"
                                    );
                                    string contenido = p.Contenido;
                                    if (contenido.Length > 50) 
                                    {
                                        contenido = contenido.Substring(0,50);
                                    }
                                    Console.WriteLine($"$Contenido:{contenido}\n");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "5":
                        
                        List<Usuario> miembrosMasPublicaciones = S.ObtenerMiembrosConMasPublicaciones();

                        Console.WriteLine("Miembros con más publicaciones:");
                        foreach (Miembro m in miembrosMasPublicaciones)
                        { 
                            Console.WriteLine($"Nombre Completo: {m.Nombre} {m.Apellido}\nEmail: {m.Email}\nFecha de nacimiento {m.fNac}");
                        }
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del programa.");
                        ejecutandose = false;
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Por favor, ingresá una opción válida.");
                        break;
                }

            Console.WriteLine();
            Console.ReadKey();
            }
        }
    }
}