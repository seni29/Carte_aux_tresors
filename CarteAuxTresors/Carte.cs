using System;
using System.Collections.Generic;
using System.Linq;

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

        //public Carte Initialiser(IList<Ligne> lignes)
        //{
        //    if (lignes[0].Type == TypeLigne.C)
        //    {
        //        var largeur = int.Parse(lignes[0].Contenu[0]);
        //        var hauteur = int.Parse(lignes[0].Contenu[1]);
        //        Cases = new Case[largeur, hauteur];

        //        for (int i = 1; i < lignes.Count; i++)
        //        {
        //            switch (lignes[i].Type)
        //            {
        //                case TypeLigne.M:
        //                    {
        //                        var axeHorizontal = int.Parse(lignes[i].Contenu[0]);
        //                        var axeVertical = int.Parse(lignes[i].Contenu[1]);
        //                        Cases[axeHorizontal, axeVertical] = new Montagne();
        //                        break;
        //                    }

        //                case TypeLigne.T:
        //                    {
        //                        var axeHorizontal = int.Parse(lignes[i].Contenu[0]);
        //                        var axeVertical = int.Parse(lignes[i].Contenu[1]);
        //                        var nombreTresors = int.Parse(lignes[i].Contenu[2]);
        //                        Cases[axeHorizontal, axeVertical] = new Tresor(nombreTresors);
        //                        break;
        //                    }
        //                case TypeLigne.A:
        //                    {
        //                        var axeHorizontal = int.Parse(lignes[i].Contenu[1]);
        //                        var axeVertical = int.Parse(lignes[i].Contenu[2]);
        //                        if (Cases[axeHorizontal, axeVertical] == null)
        //                            Cases[axeHorizontal, axeVertical] = new Plaine();
        //                        Cases[axeHorizontal, axeVertical].Occuper();
        //                        break;
        //                    }
        //            }

        //        }

        //        for (int i = 0; i < largeur; i++)
        //            for (int j = 0; j < hauteur; j++)
        //                if (Cases[i, j] == null)
        //                    Cases[i, j] = new Plaine();
        //    }
        //    return this;
        //}

        public Carte Initialiser(IList<string> lignes)
        {
            try
            {
                var ligneInitCarte = lignes.Single(x => x.StartsWith(TypeLigne.C.ToString())).Split(" - ");
                var largeur = int.Parse(ligneInitCarte[1]);
                var hauteur = int.Parse(ligneInitCarte[2]);
                Cases = new Case[largeur, hauteur];


                var lignesContenuCarte = lignes.Where(x => !x.StartsWith(TypeLigne.C.ToString()));
                foreach (var ligne in lignesContenuCarte)
                {
                    var itemsLigne = ligne.Split(" - ").ToList();
                    try
                    {
                        var type = (TypeLigne)Enum.Parse(typeof(TypeLigne), itemsLigne[0]);
                        switch (type)
                        {
                            case TypeLigne.M:
                                {
                                    var axeHorizontal = int.Parse(itemsLigne[1]);
                                    var axeVertical = int.Parse(itemsLigne[2]);
                                    Cases[axeHorizontal, axeVertical] = new Montagne();
                                    break;
                                }

                            case TypeLigne.T:
                                {
                                    var axeHorizontal = int.Parse(itemsLigne[1]);
                                    var axeVertical = int.Parse(itemsLigne[2]);
                                    var nombreTresors = int.Parse(itemsLigne[3]);
                                    Cases[axeHorizontal, axeVertical] = new Tresor(nombreTresors);
                                    break;
                                }
                            case TypeLigne.A:
                                {
                                    var axeHorizontal = int.Parse(itemsLigne[2]);
                                    var axeVertical = int.Parse(itemsLigne[3]);
                                    if (Cases[axeHorizontal, axeVertical] == null)
                                        Cases[axeHorizontal, axeVertical] = new Plaine();
                                    Cases[axeHorizontal, axeVertical].Occuper();
                                    break;
                                }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                for (int i = 0; i < largeur; i++)
                    for (int j = 0; j < hauteur; j++)
                        if (Cases[i, j] == null)
                            Cases[i, j] = new Plaine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Intialisation de la carte impossible\n" + e);
            }

            return this;
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