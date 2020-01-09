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
            var lignes = _entrepot.RecupererDonnees(@"c:\in.txt");
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

        //public IList<Ligne> Resultat()
        //{
        //    var cases = Carte.Cases;
        //    var lignes = new List<Ligne>();
        //    int largeur = cases.GetLength(0);
        //    int hauteur = cases.GetLength(1);
        //    var dimensions = new Ligne
        //    {
        //        Type = TypeLigne.C,
        //        Contenu = new List<string> { largeur.ToString(), hauteur.ToString() }
        //    };
        //    lignes.Add(dimensions);

        //    for (int i = 0; i < hauteur; i++)
        //        for (int j = 0; j < largeur; j++)
        //        {
        //            if (cases[j,i] is Montagne)
        //                lignes.Add(new Ligne
        //                {
        //                    Type = TypeLigne.M,
        //                    Contenu = new List<string> { j.ToString(), i.ToString() }
        //                });
        //            else if (cases[j,i] is Tresor)
        //            {
        //                var tresor = (Tresor)cases[j, i];
        //                if (tresor.EstLibre && tresor.NbTresors > 0)
        //                    lignes.Add(new Ligne
        //                    {
        //                        Type = TypeLigne.T,
        //                        Contenu = new List<string> { j.ToString(), i.ToString(), tresor.NbTresors.ToString() }
        //                    });
        //            }
        //        }

        //    foreach (var aventurier in Aventuriers)
        //        lignes.Add(new Ligne
        //        {
        //            Type = TypeLigne.A,
        //            Contenu = new List<string> {
        //                aventurier.Nom,
        //                aventurier.Position.AxeHorizontal.ToString(),
        //                aventurier.Position.AxeVertical.ToString(),
        //                aventurier.Orientation.ToString(),
        //                aventurier.NbTresors.ToString()
        //            }
        //        });

        //    return lignes;

        //}

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

            _entrepot.Enregistrer(lignes, @"c:\out.txt");
            return lignes;

        }

        private void InitialiserCarte(IList<string> lignes)
        {
            Carte = Carte.Instance.Initialiser(lignes);
        }

        //private void InitialiserAventuriers(IList<Ligne> lignes)
        //{
        //    var lignesAventuriers = lignes.Where(x => x.Type == TypeLigne.A);
        //    foreach (var ligne in lignesAventuriers)
        //    {
        //        var nom = ligne.Contenu[0];
        //        var axeHorizontal = int.Parse(ligne.Contenu[1]);
        //        var axeVertical = int.Parse(ligne.Contenu[2]);
        //        var orientation = (Orientation)Enum.Parse(typeof(Orientation), ligne.Contenu[3]);
        //        var sequenceMouvements = ligne.Contenu[4];
        //        var position = new Position(axeHorizontal, axeVertical);
        //        Aventuriers.Add(new Aventurier(nom, position, orientation, sequenceMouvements));
        //    }
        //}

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