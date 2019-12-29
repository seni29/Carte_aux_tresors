using System;
using System.Collections.Generic;
using System.Linq;

namespace CarteAuxTresors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var partie = new Partie();
            //partie.Initialiser();

            //var carte = new Carte();
            //carte.Initialiser();




        }

    }

    public class Partie
    {
        public Partie()
        {
        }

        public int NombreDeTours
        {
            get
            {
                return Aventuriers.Select(x => x.Sequence).Select(y => y.Length).Max();
            }
        }
        IList<Aventurier> Aventuriers { get; set; }
        Case[,] Carte { get; set; }

        public void Initialiser(IList<Ligne> lignes)
        {
                if (lignes[0].TypeCase == 'C')
                {
                    var largeurCarte = int.Parse(lignes[0].ContenuCase[0]);
                    var hauteurCarte = int.Parse(lignes[0].ContenuCase[1]);
                    Carte = new Case[largeurCarte, hauteurCarte];

                    for (int i = 1; i < lignes.Count; i++)
                    {
                        if (lignes[i].TypeCase == 'M')
                        {
                            var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                            var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                            Carte[axeHorizontal, axeVertical] = new Montagne();
                        }
                        else if (lignes[i].TypeCase == 'T')
                        {
                            var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                            var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                            var nombreTresors = int.Parse(lignes[i].ContenuCase[2]);
                            Carte[axeHorizontal, axeVertical] = new Tresor(nombreTresors);
                        }
                        else if (lignes[i].TypeCase == 'A')
                        {
                            var nom = lignes[i].ContenuCase[0];
                            var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                            var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                            var orientation = (Orientation)Enum.Parse(typeof(Orientation), lignes[i].ContenuCase[2]);
                            var sequenceMouvements = lignes[i].ContenuCase[3];
                            var aventurier = new Aventurier(nom, axeHorizontal, axeVertical, orientation, sequenceMouvements);
                            Carte[axeHorizontal, axeVertical] = aventurier;
                            Aventuriers.Add(aventurier);
                        }
              

                }
                    //Cases[1, 0] = new Montagne();
                    //Cases[1, 2] = new Montagne();
                    //Cases[3, 0] = new Tresor(2);
                    //Cases[3, 1] = new Tresor(3);

                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 4; j++)
                            if (Carte[i, j] == null)
                                Carte[i, j] = new Plaine();
                }
            
        }
        public void Jouer()
        {
            for (int i = 0; i < NombreDeTours; i++)
            {
                foreach (Aventurier aventurier in Aventuriers)
                {
                    aventurier.JouerTour(i);
                }
            }

        }
    }

    public class Carte
    {
        //static private Carte _instance;
        //static public Carte Instance
        //{
        //    get { return _instance ?? (_instance = new Carte()); }
        //}
              
        Case[,] Cases { get; set; }

        public void Initialiser(IList<Ligne> lignes)
        {
            if (lignes[0].TypeCase == 'C')
            {
                var largeurCarte = int.Parse(lignes[0].ContenuCase[0]);
                var hauteurCarte = int.Parse(lignes[0].ContenuCase[1]);
                Cases = new Case[largeurCarte, hauteurCarte] ;

                for (int i = 1; i<lignes.Count; i++)
                {
                    if (lignes[i].TypeCase == 'M')
                    {
                        var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                        var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                        Cases[axeHorizontal, axeVertical] = new Montagne();
                    }else if(lignes[i].TypeCase == 'T')
                    {
                        var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                        var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                        var nombreTresors = int.Parse(lignes[i].ContenuCase[2]);
                        Cases[axeHorizontal, axeVertical] = new Tresor(nombreTresors);
                    }else if (lignes[i].TypeCase == 'A')
                    {
                        var nom = lignes[i].ContenuCase[0];
                        var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                        var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                        var orientation = (Orientation)Enum.Parse(typeof(Orientation), lignes[i].ContenuCase[2]);
                        var sequenceMouvements = lignes[i].ContenuCase[3];
                        Cases[axeHorizontal, axeVertical] = new Aventurier(nom, axeHorizontal, axeVertical, orientation, sequenceMouvements);
                    }

                }
                //Cases[1, 0] = new Montagne();
                //Cases[1, 2] = new Montagne();
                //Cases[3, 0] = new Tresor(2);
                //Cases[3, 1] = new Tresor(3);

                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 4; j++)
                        if (Cases[i, j] == null)
                            Cases[i, j] = new Plaine();
            }
        }

       


    }

    public class Ligne
    {
        public Char TypeCase { get; set; }
        public List<string> ContenuCase { get; set; }
    }

    public abstract class Case
    {

    }

    public class Plaine : Case
    {

    }

    public class Montagne : Case
    {

    }


    public class Tresor : Case
    {
        private readonly int _nbTresors;

        public Tresor(int nbTresors)
        {
            _nbTresors = nbTresors;
        }
    }


}