namespace TeVasAMorir
{
    public class Program
    {
        public static void Main()
        {
            Laberinto laberinto = new Laberinto(21); // Usa un tamaño impar para que funcione bien con la accesibilidad
            laberinto.ImprimirTablero();
        }
    }
}