namespace TeVasAMorir
{
    public class Laberinto
    {
        public Celda[,] Tablero {get; private set;}
        private int tamaño;
        private Random random = new Random();
        private static readonly int[] dx = {0,0,1,-1};
        private static readonly int[] dy = {1,-1,0,0};

        public Laberinto(int tamaño)
        {
            this.tamaño = tamaño;
            Tablero = new Celda[tamaño,tamaño];
            InicializarTablero();
            GenerarLaberinto();
            AbrirEspaciosDeFichas();
        } 

        private void InicializarTablero()
        {
            for(int y = 0; y<tamaño; y++)
            {
                for(int x = 0; x<tamaño; x++)
                {
                    Tablero[x,y] = new Celda(x,y);
                }
            }
        }

        private void GenerarLaberinto()
        {
            Stack<(int,int)> pila = new Stack<(int, int)>();
            int xInicial = random.Next(tamaño);
            int yInicial = random.Next(tamaño);

            Tablero[xInicial,yInicial].Visitada = true;
            pila.Push((xInicial,yInicial));

            while(pila.Count>0)
            {
                var (x,y) = pila.Peek();
                List<(int,int)> vecinos = ObtenerVecinosNoVisitados(x,y);

                if(vecinos.Count>0)
                {
                    var (nx,ny) = vecinos[random.Next(vecinos.Count)];
                    Tablero[nx,ny].Visitada = true;
                    QuitarPared(x,y,nx,ny);
                    pila.Push((nx,ny));
                }
                else
                {
                    pila.Pop();
                }
            }
            
            PonerTrampas();
        }

        private List<(int,int)> ObtenerVecinosNoVisitados(int x, int y)
        {
            List<(int,int)> vecinos = new List<(int, int)>();
            for(int i = 0; i<4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                
                if(DentroDelLaberinto(nx,ny) && !Tablero[nx,ny].Visitada)
                {
                    vecinos.Add((nx,ny));
                }
            }
            return vecinos;
        }

        private void QuitarPared(int x, int y, int nx, int ny)
        {
            int wx = (x + nx)/2;
            int wy = (y + ny)/2;
            Tablero[wx,wy].EsObstaculo = false;
        }

        private bool DentroDelLaberinto(int x, int y)
        {
            return x >= 0 && x<tamaño && y >= 0 && y<tamaño;
        }

        private void PonerTrampas()
        {
            for(int y = 0; y<tamaño; y++)
            {
                for(int x = 0; x<tamaño; x++)
                {
                    if(Tablero[x,y].Visitada && random.NextDouble()<0.10) //Probablilidad del 10%
                    {
                      Tablero[x,y].AgregarTrampa("Una trampa ahi");
                    }
                }
            }
        }

        private void AbrirEspaciosDeFichas()
        {
            Tablero[0,0].EsObstaculo = Tablero[0,0].TieneTrampa = false;
            Tablero[0,1].EsObstaculo = Tablero[0,1].TieneTrampa = false;
            Tablero[1,0].EsObstaculo = Tablero[1,0].TieneTrampa = false;
            Tablero[1,1].EsObstaculo = Tablero[1,1].TieneTrampa = false;
            Tablero[tamaño-1,tamaño-1].EsObstaculo = Tablero[tamaño-1,tamaño-1].TieneTrampa = false;
            Tablero[tamaño-2,tamaño-1].EsObstaculo = Tablero[tamaño-2,tamaño-1].TieneTrampa = false;
            Tablero[tamaño-1,tamaño-2].EsObstaculo = Tablero[tamaño-1,tamaño-2].TieneTrampa = false;
            Tablero[tamaño-2,tamaño-2].EsObstaculo = Tablero[tamaño-2,tamaño-2].TieneTrampa = false;
            Tablero[tamaño-1,0].EsObstaculo = Tablero[tamaño-1,0].TieneTrampa = false;
            Tablero[tamaño-1,1].EsObstaculo = Tablero[tamaño-1,1].TieneTrampa = false;
            Tablero[tamaño-2,0].EsObstaculo = Tablero[tamaño-2,0].TieneTrampa = false;
            Tablero[tamaño-2,1].EsObstaculo = Tablero[tamaño-2,1].TieneTrampa = false;
            Tablero[0,tamaño-1].EsObstaculo = Tablero[0,tamaño-1].TieneTrampa = false;
            Tablero[1,tamaño-1].EsObstaculo = Tablero[1,tamaño-1].TieneTrampa = false;
            Tablero[0,tamaño-2].EsObstaculo = Tablero[0,tamaño-2].TieneTrampa = false;
            Tablero[1,tamaño-2].EsObstaculo = Tablero[1,tamaño-2].TieneTrampa = false;
        }

        public void ImprimirTablero()
        {
            for(int y = 0; y<tamaño; y++)
            {
                for(int x = 0; x<tamaño; x++)
                {
                    Tablero[x,y].ImprimirCelda();
                }
                Console.WriteLine();
            }
        }

    }
}