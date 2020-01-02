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
            for (int i = 0; i < largeur; i++)
                for (int j = 0; j < hauteur; j++)
                        Cases[i,j] = new Plaine();
        }

        public void Positionner(Coordonnees coordonnees, Case item)
        {
            Cases[coordonnees.AxeHorizontal, coordonnees.AxeVertical] = item;
        }

        public Case RecupererCase(Coordonnees coordonnees)
        {
            return Cases[coordonnees.AxeHorizontal, coordonnees.AxeVertical];
        }
    }


}