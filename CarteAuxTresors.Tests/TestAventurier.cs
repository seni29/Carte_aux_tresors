using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarteAuxTresors.Tests
{
    [TestClass]
    public class TestAventurier
    {
        private Carte _carte;

        [TestInitialize]
        public void Setup()
        {
            _carte = Carte.Instance.Initialiser(
              //new List<Ligne>
              //{
              //      new Ligne{Type=TypeLigne.C, Contenu=new List<string>{"3", "4"}},
              //      new Ligne{Type=TypeLigne.M, Contenu=new List<string>{"1", "0"}},
              //      new Ligne{Type=TypeLigne.M, Contenu=new List<string>{"2", "1"}},
              //      new Ligne{Type=TypeLigne.T, Contenu=new List<string>{"0", "3", "2"}},
              //      new Ligne{Type=TypeLigne.T, Contenu=new List<string>{"1", "3", "3"}},
              //      new Ligne{Type=TypeLigne.A, Contenu=new List<string>{"Indiana", "1", "1", "S", "AADADA"}},
              //      new Ligne{Type=TypeLigne.A, Contenu=new List<string>{"Luca", "0", "1", "S", "AADADA"}},
              //});

            new List<string>
              {
                  "C - 3 - 4",
                  "M - 1 - 0",
                  "M - 2 - 1",
                  "T - 0 - 3 - 2",
                  "T - 1 - 3 - 3",
                  "A - Indiana - 1 - 1 - S - AADADA",
                  "A - Lara - 0 - 1 - S - AADADA"

              });


        }

        [DataRow(Orientation.N, Action.TourADroite, Orientation.E)]
        [DataRow(Orientation.E, Action.TourADroite, Orientation.S)]
        [DataRow(Orientation.S, Action.TourADroite, Orientation.O)]
        [DataRow(Orientation.O, Action.TourADroite, Orientation.N)]
        [DataRow(Orientation.N, Action.TourAGauche, Orientation.O)]
        [DataRow(Orientation.O, Action.TourAGauche, Orientation.S)]
        [DataRow(Orientation.S, Action.TourAGauche, Orientation.E)]
        [DataRow(Orientation.E, Action.TourAGauche, Orientation.N)]
        [DataTestMethod]
        public void VerifierChangementDirection(Orientation debut, Action action, Orientation fin)
        {
            var aventurier = new Aventurier("Indiana", new Position(1, 1), debut, "AADADA");
            aventurier.Tourner(action);
            Assert.AreEqual(aventurier.Orientation, fin);
        }



        [DataRow(Orientation.N, 2, 3, 2, 2)]
        [DataRow(Orientation.E, 1, 2, 2, 2)]
        [DataRow(Orientation.S, 1, 1, 1, 2)]
        [DataRow(Orientation.O, 1, 2, 0, 2)]
        [DataTestMethod]
        public void AventurierPeutAvancer(Orientation orientation, int horizDep, int vertDep, int horizDest, int vertDest)
        {
            var aventurier = new Aventurier("Indiana", new Position(horizDep, vertDep), orientation, "AADADA");

            aventurier.Avancer(_carte);

            Assert.AreEqual(horizDest, aventurier.Position.AxeHorizontal);
            Assert.AreEqual(vertDest, aventurier.Position.AxeVertical);
            var caseDepart = _carte.Recuperer(new Position(horizDep, vertDep));
            var caseArrivee = _carte.Recuperer(new Position(horizDest, vertDest));
            Assert.IsTrue(caseDepart.EstLibre);
            Assert.IsFalse(caseArrivee.EstLibre);
        }

        [DataRow(Orientation.N, 1, 1)]
        [DataRow(Orientation.N, 0, 0)]
        [DataRow(Orientation.S, 2, 3)]
        [DataRow(Orientation.S, 0, 0)]
        [DataTestMethod]
        public void AventurierNePeutPasAvancer(Orientation orientation, int horizDep, int vertDep)
        {
            var aventurier = new Aventurier("Indiana", new Position(horizDep, vertDep), orientation, "AADADA");

            aventurier.Avancer(_carte);
            Assert.AreEqual(horizDep, aventurier.Position.AxeHorizontal);
            Assert.AreEqual(vertDep, aventurier.Position.AxeVertical);
        }

        [DataTestMethod]
        public void VerifierCollecteTresor()
        {
            var aventurier = new Aventurier("Indiana", new Position(1, 2), Orientation.S, "AADADA");
            Assert.AreEqual(0, aventurier.NbTresors);
            aventurier.Avancer(_carte);
            Assert.AreEqual(1, aventurier.NbTresors);

            var tresor = (Tresor)_carte.Recuperer(new Position(1, 3));
            Assert.AreEqual(2, tresor.NbTresors);
        }
    }
}
