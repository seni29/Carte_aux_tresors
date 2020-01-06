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

        public IList<Ligne> Resultat()
        {
            var cases = Carte.Cases;
            var lignes = new List<Ligne>();
            int largeur = cases.GetLength(0);
            int hauteur = cases.GetLength(1);
            var dimensions = new Ligne
            {
                Type = TypeLigne.Carte,
                ContenuCase = new List<string> { largeur.ToString(), hauteur.ToString() }
            };
            lignes.Add(dimensions);

            for (int i = 0; i < hauteur; i++)
                for (int j = 0; j < largeur; j++)
                {
                    if (cases[j,i] is Montagne)
                        lignes.Add(new Ligne
                        {
                            Type = TypeLigne.Montagne,
                            ContenuCase = new List<string> { j.ToString(), i.ToString() }
                        });
                    else if (cases[j,i] is Tresor)
                    {
                        var tresor = (Tresor)cases[j, i];
                        if (tresor.EstLibre && tresor.NbTresors > 0)
                            lignes.Add(new Ligne
                            {
                                Type = TypeLigne.Tresor,
                                ContenuCase = new List<string> { j.ToString(), i.ToString(), tresor.NbTresors.ToString() }
                            });
                    }
                }

            foreach (var aventurier in Aventuriers)
                lignes.Add(new Ligne
                {
                    Type = TypeLigne.Aventurier,
                    ContenuCase = new List<string> {
                        aventurier.Nom,
                        aventurier.Position.AxeHorizontal.ToString(),
                        aventurier.Position.AxeVertical.ToString(),
                        aventurier.Orientation.ToString(),
                        aventurier.NbTresors.ToString()
                    }
                });

            return lignes;

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



    }

}