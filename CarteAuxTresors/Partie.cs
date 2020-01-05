using System;
using System.Collections.Generic;
using System.Linq;

namespace CarteAuxTresors
{
    public class Partie
    {
        private IFournisseur _fournisseur;

        public int NombreDeTours
        {
            get
            {
                return Aventuriers.Select(x => x.Sequence).Select(y => y.Length).Min();
            }
        }
        public IList<Aventurier> Aventuriers { get; set; } = new List<Aventurier>();
        public Carte Carte { get; set; }

        public Partie(IFournisseur fournisseur)
        {
            _fournisseur = fournisseur;
        }
        public Partie Initialiser()
        {
            var lignes = _fournisseur.RecupererDonnees();
            InitialiserCarte(lignes);
            InitialiserAventuriers(lignes);
            return this;
        }

        private void InitialiserCarte(IList<Ligne> lignes)
        {
            Carte = Carte.Instance.Initialiser(lignes);
        }

        private void InitialiserAventuriers(IList<Ligne> lignes)
        {
            var lignesAventuriers = lignes.Where(x => x.Type == TypeLigne.Aventurier);
            foreach (var ligne in lignesAventuriers)
            {
                var nom = ligne.ContenuCase[0];
                var axeHorizontal = int.Parse(ligne.ContenuCase[1]);
                var axeVertical = int.Parse(ligne.ContenuCase[2]);
                var orientation = (Orientation)Enum.Parse(typeof(Orientation), ligne.ContenuCase[3]);
                var sequenceMouvements = ligne.ContenuCase[4];
                var position = new Position(axeHorizontal, axeVertical);
                Aventuriers.Add(new Aventurier(nom, position, orientation, sequenceMouvements));
            }
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

    }
   
}