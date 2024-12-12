namespace TeVasAMorir
{
    public class Celda
    {
        public int X {get; set;}
        public int Y {get; set;}
        public bool EsObstaculo{get; set;}
        public bool TieneTrampa{get; set;}
        public bool Visitada{get; set;}
        public List<string> Trampas {get; set;}= new List<string>();

        public Celda(int x, int y)
        {
            X=x;
            Y=y;
            TieneTrampa=false;
            EsObstaculo=true;
            Visitada=false;
        }

        public void AgregarTrampa(string trampa)
        {
            TieneTrampa=true;
            Trampas.Add(trampa);
        }

        public void ImprimirCelda()
        {
            if(TieneTrampa)
            {
                Console.Write("T");
            }
            else if(EsObstaculo)
            {
                Console.Write("O");
            }
            else
            {
                Console.Write(" ");
            }
        }
        
    }
}