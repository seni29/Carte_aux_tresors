using System;
using System.Collections.Generic;
using System.Linq;

namespace CarteAuxTresors
{
    public class Partie
    {
        public int NombreDeTours
        {
            get
            {
                return Aventuriers.Select(x => x.Sequence).Select(y => y.Length).Min();
            }
        }
        IList<Aventurier> Aventuriers { get; set; }
        Carte Carte { get; set; }

        public void Initialiser(IList<Ligne> lignes)
        {
            if (lignes[0].TypeCase == 'C')
            {
                var largeurCarte = int.Parse(lignes[0].ContenuCase[0]);
                var hauteurCarte = int.Parse(lignes[0].ContenuCase[1]);
                Carte = Carte.Instance;
                Carte.Initialiser(largeurCarte, hauteurCarte);

                for (int i = 1; i < lignes.Count; i++)
                {
                    if (lignes[i].TypeCase == 'M')
                    {
                        var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                        var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                        //Carte[axeHorizontal, axeVertical] = new Montagne();
                        Carte.Positionner(new Position(axeHorizontal, axeVertical), new Montagne());
                    }
                    else if (lignes[i].TypeCase == 'T')
                    {
                        var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                        var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                        var nombreTresors = int.Parse(lignes[i].ContenuCase[2]);
                        //Carte[axeHorizontal, axeVertical] = new Tresor(nombreTresors);
                        Carte.Positionner(new Position(axeHorizontal, axeVertical), new Tresor(nombreTresors));
                    }
                    else if (lignes[i].TypeCase == 'A')
                    {
                        var nom = lignes[i].ContenuCase[0];
                        var axeHorizontal = int.Parse(lignes[i].ContenuCase[0]);
                        var axeVertical = int.Parse(lignes[i].ContenuCase[1]);
                        var orientation = (Orientation)Enum.Parse(typeof(Orientation), lignes[i].ContenuCase[2]);
                        var sequenceMouvements = lignes[i].ContenuCase[3];
                        Aventuriers.Add(new Aventurier(nom, new Position(axeHorizontal, axeVertical), orientation, sequenceMouvements));
                        //Aventuriers.Add(new Aventurier(Carte, new InformationsAventurier(
                        //    nom, new Position(axeHorizontal, axeVertical), orientation, sequenceMouvements))
                    }
                }
            }

            //Cases[1, 0] = new Montagne();
            //Cases[1, 2] = new Montagne();
            //Cases[3, 0] = new Tresor(2);
            //Cases[3, 1] = new Tresor(3);

            
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
   
}