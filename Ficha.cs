namespace TeVasAMorir
{
    class Ficha
    {
        public string Tipo { get; set; }
        public int xPosicion { get; set; }
        public int yPosicion { get; set; }

        public Ficha(string tipo, int xposicion, int yposicion)
        {
            Tipo = tipo;
            xPosicion = xposicion;
            yPosicion = yposicion;
        }

        public void MoverFicha(int xnuevo, int ynuevo)
        {
            xPosicion = xnuevo;
            yPosicion = ynuevo;
        }

        public override string ToString()
        {
            return $"Ficha: {Tipo} en ({xPosicion}, {yPosicion})";
        }

    }
}