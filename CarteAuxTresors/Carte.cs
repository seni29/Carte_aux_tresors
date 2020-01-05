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

        public Carte Initialiser(IList<Ligne> lignes)
        {
            if (lignes[0].Type == TypeLigne.Carte)
            {
                var largeur = int.Parse(lignes[0].ContenuCase[0]);
                var hauteur = int.Parse(lignes[0].ContenuCase[1]);
                Cases = new Case[largeur, hauteur];

                for (int i = 1; i < lignes.Count; i++)
                {
                    switch (lignes[i].Type)
                    {
                        case TypeLigne.Montagne:
                            {
                                var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                                var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                                Cases[axeHorizontal, axeVertical] = new Montagne();
                                break;
                            }

                        case TypeLigne.Tresor:
                            {
                                var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                                var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                                var nombreTresors = int.Parse(lignes[i].ContenuCase[2]);
                                Cases[axeHorizontal, axeVertical] = new Tresor(nombreTresors);
                                break;
                            }
                        case TypeLigne.Aventurier:
                            {
                                var axeHorizontal = int.Parse(lignes[i].ContenuCase[1]);
                                var axeVertical = int.Parse(lignes[i].ContenuCase[2]);
                                if (Cases[axeHorizontal, axeVertical] == null)
                                    Cases[axeHorizontal, axeVertical] = new Plaine();
                                Cases[axeHorizontal, axeVertical].Occuper();
                                break;
                            }
                    }

                }

                for (int i = 0; i < largeur; i++)
                    for (int j = 0; j < hauteur; j++)
                        if (Cases[i, j] == null)
                            Cases[i, j] = new Plaine();
            }
            return this;
        }
        //public void Initialiser(int largeur, int hauteur)
        //{
        //    Cases = new Case[largeur, hauteur];
            
        //}

        //public void Positionner(Position position, Case element)
        //{
        //    Cases[position.AxeHorizontal, position.AxeHorizontal] = element;
        //}

       

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