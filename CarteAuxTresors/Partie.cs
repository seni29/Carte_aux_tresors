﻿using System;
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
            InitialiserCarte(lignes);
            InitialiserAventuriers(lignes);

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
                var axeHorizontal = int.Parse(ligne.ContenuCase[0]);
                var axeVertical = int.Parse(ligne.ContenuCase[1]);
                var orientation = (Orientation)Enum.Parse(typeof(Orientation), ligne.ContenuCase[2]);
                var sequenceMouvements = ligne.ContenuCase[3];
                var position = new Position(axeHorizontal, axeVertical);
                Aventuriers.Add(new Aventurier(nom, position, orientation, sequenceMouvements));
                Carte.Recuperer(position).Occuper();
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