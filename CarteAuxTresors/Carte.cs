using System;
using System.Collections.Generic;

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

        public void Initialiser(IList<Ligne> lignes)
        {

            if (lignes[0].Type == 'C')
            {
                var largeur = int.Parse(lignes[0].ContenuCase[0]);
                var hauteur = int.Parse(lignes[0].ContenuCase[1]);
                Cases = new Case[largeur, hauteur];

                for (int i = 1; i < lignes.Count; i++)
                {
                    if (lignes[i].Type == 'M')
                    {
                        var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                        var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                        Cases[axeHorizontal, axeVertical] = new Montagne();
                    }
                    else if (lignes[i].Type == 'T')
                    {
                        var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                        var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                        var nombreTresors = int.Parse(lignes[i].ContenuCase[2]);
                        Cases[axeHorizontal, axeVertical] = new Tresor(nombreTresors);
                    }
                   
                }

                for (int i = 0; i < largeur; i++)
                    for (int j = 0; j < hauteur; j++)
                        if (Cases[i, j] == null)
                            Cases[i, j] = new Plaine();
            }
        }
        //public void Initialiser(int largeur, int hauteur)
        //{
        //    Cases = new Case[largeur, hauteur];
            
        //}

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