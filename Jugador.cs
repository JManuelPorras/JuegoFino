namespace TeVasAMorir
{
    class Jugador
    {
        public string Nombre { get; set; }
        public int Puntuacion { get; set; }
        public List<Ficha> Fichas { get; set; }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Puntuacion = 0;
            Fichas = new List<Ficha>();
        }

        public void CogerFichas(Ficha ficha)
        {
            Fichas.Add(ficha);
        }

        public void IncrementarPuntuacion(int puntos)
        {
            Puntuacion += puntos;
        }

        public override string ToString()
        {
            return $"Jugador: {Nombre} ,Puntuacion: {Puntuacion} ,Fichas: {Fichas.Count}";
        }
    }
}