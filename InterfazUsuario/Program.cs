namespace InterfazUsuario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((new List<String> { ".jpg", ".png" }).Any(sufijo => "ds.jpeg".EndsWith(sufijo)));
            Console.ReadKey();
        }
    }
}