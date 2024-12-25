namespace TeVasAMorir
{
    public class Celda
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool EsObstaculo { get; set; }
        public bool Visitada { get; set; }
        public int Distancia { get; set; }

        public Celda(int x, int y)
        {
            X = x;
            Y = y;
            EsObstaculo = false;  // Inicializa sin obstáculos
            Visitada = false;
            Distancia = -1;
        }

        public void ImprimirCelda()
        {
            if (EsObstaculo)
            {
                Console.Write("□");
            }
            else
            {
                Console.Write(" ");
            }
        }
    }

}