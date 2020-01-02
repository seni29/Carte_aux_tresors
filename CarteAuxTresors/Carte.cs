using System;

namespace CarteAuxTresors
{
    public class Carte
    {
        static private Carte _instance;
        static public Carte Instance
        {
            get { return _instance ?? (_instance = new Carte()); }
        }

        public Case[,] Cases { get; set; }

        public void Initialiser(int largeur, int hauteur)
        {
            Cases = new Case[largeur, hauteur];
            
        }

        public void Positionner(Position position, Case element)
        {
            Cases[position.AxeHorizontal, position.AxeHorizontal] = element;
        }

        public Case Recuperer(Position position)
        {
            try
            {
                return Cases[position.AxeHorizontal, position.AxeVertical];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
    }


}