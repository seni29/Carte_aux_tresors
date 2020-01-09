using System;
using System.Collections.Generic;
using System.Linq;

namespace CarteAuxTresors
{
    public class Partie
    {
        private IEntrepot _entrepot;

        public int NombreDeTours
        {
            get
            {
                return Aventuriers.Select(x => x.Sequence).Select(y => y.Length).Min();
            }
        }
        public IList<Aventurier> Aventuriers { get; set; } = new List<Aventurier>();
        public Carte Carte { get; set; }

        public Partie(IEntrepot entrepot)
        {
            _entrepot = entrepot;
        }
        public Partie Initialiser()
        {
            var lignes = _entrepot.RecupererDonnees();
            InitialiserCarte(lignes);
            InitialiserAventuriers(lignes);
            return this;
        }

        public void Jouer()
        {
            for (int i = 0; i < NombreDeTours; i++)
            {
                foreach (Aventurier aventurier in Aventuriers)
                {
                    aventurier.JouerTour(Carte, i);
                }
            }

        }

        public IList<string> Resultat()
        {
            var cases = Carte.Cases;
            var lignes = new List<string>();
            int largeur = cases.GetLength(0);
            int hauteur = cases.GetLength(1);

            lignes.Add(TypeLigne.C + " - " + largeur + " - " + hauteur);

            for (int i = 0; i < hauteur; i++)
                for (int j = 0; j < largeur; j++)
                {
                    if (cases[j, i] is Montagne)
                        lignes.Add(TypeLigne.M + " - " + j + " - " + i);
                    else if (cases[j, i] is Tresor)
                    {
                        var tresor = (Tresor)cases[j, i];
                        if (tresor.EstLibre && tresor.NbTresors > 0)
                            lignes.Add(TypeLigne.T + " - " + j + " - " + i + " - " + tresor.NbTresors);
                    }
                }

            foreach (var aventurier in Aventuriers)
                lignes.Add(TypeLigne.A + " - " + aventurier.Nom + " - " + aventurier.Position.AxeHorizontal + " - " 
                    + aventurier.Position.AxeVertical + " - " + aventurier.Orientation + " - " + aventurier.NbTresors);

            _entrepot.Enregistrer(lignes);
            return lignes;

        }

        private void InitialiserCarte(IList<string> lignes)
        {
            Carte = Carte.Instance.Initialiser(lignes);
        }

        private void InitialiserAventuriers(IList<string> lignes)
        {
            var lignesAventuriers = lignes.Where(x => x.StartsWith(TypeLigne.A.ToString()));
            foreach (var ligne in lignesAventuriers)
            {
                var itemsLigne = ligne.Split(" - ");
                var nom = itemsLigne[1];
                var axeHorizontal = int.Parse(itemsLigne[2]);
                var axeVertical = int.Parse(itemsLigne[3]);
                var orientation = (Orientation)Enum.Parse(typeof(Orientation), itemsLigne[4]);
                var sequenceMouvements = itemsLigne[5];
                var position = new Position(axeHorizontal, axeVertical);
                Aventuriers.Add(new Aventurier(nom, position, orientation, sequenceMouvements));
            }
        }



    }

}