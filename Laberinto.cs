namespace TeVasAMorir
{

    public class Laberinto
    {
        public Celda[,] Tablero { get; private set; }
        private int tamano;
        private Random random = new Random();
        private static readonly int[] dx = { 0, 0, 1, -1 };
        private static readonly int[] dy = { 1, -1, 0, 0 };

        public Laberinto(int tamano)
        {
            this.tamano = tamano;
            Tablero = new Celda[tamano, tamano];
            InicializarTablero();
            ColocarObstaculos();
            GenerarLaberinto();
            CrearLaberinto();
        }



        private void InicializarTablero()
        {
            for (int y = 0; y < tamano; y++)
            {
                for (int x = 0; x < tamano; x++)
                {
                    Tablero[x, y] = new Celda(x, y);
                }
            }
        }

        private void GenerarLaberinto()
        {
            for (int x = 0; x < tamano; x++)
            {
                for (int y = 0; y < tamano; y++)
                {
                    if (Tablero[x, y].EsObstaculo == true)
                    {
                        Tablero[x, y].Visitada = false;
                    }
                }
            }
        }

        private void CrearLaberinto()
        {

            // Movimientos posibles (arriba, abajo, izquierda, derecha)
            int[][] movimientos = new int[][]
            {
            new int[] {-1, 0},
            new int[] {1, 0},
            new int[] {0, -1},
            new int[] {0, 1}
            };

            Random random = new Random();

            bool EsValido(int x, int y)
            {
                if (x < 0 || y < 0 || x >= tamano || y >= tamano || !Tablero[x, y].EsObstaculo)
                    return false;

                // Verificar que tenga como máximo un pasaje adyacente
                int pasajes = 0;
                foreach (var movimiento in movimientos)
                {
                    int nx = x + movimiento[0];
                    int ny = y + movimiento[1];
                    if (nx >= 0 && ny >= 0 && nx < tamano && ny < tamano && !Tablero[nx, ny].EsObstaculo)
                    {
                        pasajes++;
                    }
                }
                return pasajes <= 1;
            }

            void Generar(int x, int y)
            {
                Tablero[x, y].EsObstaculo = false;

                // Aleatorizar los movimientos
                for (int i = movimientos.Length - 1; i > 0; i--)
                {
                    int j = random.Next(i + 1);
                    var temp = movimientos[i];
                    movimientos[i] = movimientos[j];
                    movimientos[j] = temp;
                }

                foreach (var movimiento in movimientos)
                {
                    int nx = x + movimiento[0] * 2;
                    int ny = y + movimiento[1] * 2;
                    if (EsValido(nx, ny))
                    {
                        // Romper la pared intermedia
                        Tablero[x + movimiento[0], y + movimiento[1]].EsObstaculo = false;
                        Generar(nx, ny);
                    }
                }
            }

            // Comenzar desde una esquina (0, 0)
            Generar(0, 0);

            // Asegurar que las celdas accesibles estén conectadas desde las esquinas
            int[][] esquinas = new int[][]
            {
            new int[] {0, 0},
            new int[] {0, tamano - 1},
            new int[] {tamano - 1, 0},
            new int[] {tamano - 1, tamano - 1}
            };

            foreach (var esquina in esquinas)
            {
                Generar(esquina[0], esquina[1]);
            }

        }

        private void ColocarObstaculos()
        {
            // Inicializamos todas las celdas no como visitadas y sin obstáculo
            for (int y = 0; y < tamano; y++)
            {
                for (int x = 0; x < tamano; x++)
                {
                    Tablero[x, y].Visitada = false;
                    Tablero[x, y].EsObstaculo = false;
                }
            }

            var objetivo = new List<(int, int)>
        {
            (tamano / 2, tamano / 2),
            (tamano / 2, 0),
            (tamano / 2, tamano - 1)
        };

            var esquinas = new List<(int, int)>
        {
            (0, 0),
            (0, tamano - 1),
            (tamano - 1, 0),
            (tamano - 1, tamano - 1)
        };

            while (true)
            {
                bool todasVisitadas = true;
                for (int y = 0; y < tamano; y++)
                {
                    for (int x = 0; x < tamano; x++)
                    {
                        if (!Tablero[x, y].Visitada && !Tablero[x, y].EsObstaculo)
                        {
                            todasVisitadas = false;
                            break;
                        }
                    }
                    if (!todasVisitadas)
                        break;
                }

                if (todasVisitadas)
                    break;

                int xObs, yObs;
                do
                {
                    xObs = random.Next(tamano);
                    yObs = random.Next(tamano);
                } while (Tablero[xObs, yObs].EsObstaculo || Tablero[xObs, yObs].Visitada);

                Tablero[xObs, yObs].EsObstaculo = true;

                if (!EsAccesibleDesdeEsquinas(objetivo))
                {
                    Tablero[xObs, yObs].EsObstaculo = false;
                    Tablero[xObs, yObs].Visitada = true;

                }
            }
        }

        private bool EsAccesibleDesdeEsquinas(List<(int, int)> objetivos)
        {
            foreach (var objetivo in objetivos)
            {
                foreach (var esquina in new List<(int, int)>
            {
                (0, 0),
                (0, tamano - 1),
                (tamano - 1, 0),
                (tamano - 1, tamano - 1)
            })
                {
                    if (!AlgoritmoDeLee(esquina.Item1, esquina.Item2, objetivo.Item1, objetivo.Item2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        public bool AlgoritmoDeLee(int startX, int startY, int endX, int endY)
        {
            if (Tablero[startX, startY].EsObstaculo || Tablero[endX, endY].EsObstaculo)
            {
                return false;
            }

            Queue<(int, int)> cola = new Queue<(int, int)>();

            Tablero[startX, startY].Visitada = true;
            Tablero[startX, startY].Distancia = 0;
            cola.Enqueue((startX, startY));
            ResetVisitado();
            while (cola.Count > 0)
            {
                var (x, y) = cola.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];
                    if (DentroDelLaberinto(nx, ny) && !Tablero[nx, ny].Visitada && !Tablero[nx, ny].EsObstaculo)
                    {
                        Tablero[nx, ny].Visitada = true;
                        Tablero[nx, ny].Distancia = Tablero[x, y].Distancia + 1;
                        cola.Enqueue((nx, ny));
                        if (nx == endX && ny == endY)
                        {
                            return true; // Ruta encontrada
                        }
                    }
                }
            }

            return false; // No se encontró una ruta
        }

        private void ResetVisitado()
        {
            for (int y = 0; y < tamano; y++)
            {
                for (int x = 0; x < tamano; x++)
                {
                    Tablero[x, y].Visitada = false;
                    Tablero[x, y].Distancia = -1;
                }
            }
        }

        private bool DentroDelLaberinto(int x, int y)
        {
            return x >= 0 && x < tamano && y >= 0 && y < tamano;
        }

        public void ImprimirTablero()
        {
            for (int y = 0; y < tamano; y++)
            {
                for (int x = 0; x < tamano; x++)
                {
                    Tablero[x, y].ImprimirCelda();
                }
                Console.WriteLine();
            }
        }
    }


}