using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarteAuxTresors.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Carte _carte;

        [TestInitialize]
        public void Setup()
        {
            _carte = Carte.Instance.Initialiser(
              new List<Ligne>
              {
                    new Ligne{Type=TypeLigne.Carte, ContenuCase=new List<string>{"3", "4"}},
                    new Ligne{Type=TypeLigne.Montagne, ContenuCase=new List<string>{"1", "0"}},
                    new Ligne{Type=TypeLigne.Montagne, ContenuCase=new List<string>{"2", "1"}},
                    new Ligne{Type=TypeLigne.Tresor, ContenuCase=new List<string>{"0", "3", "2"}},
                    new Ligne{Type=TypeLigne.Tresor, ContenuCase=new List<string>{"1", "3", "3"}},
                  //new Ligne{Type='A', ContenuCase=new List<string>{"Indiana", "1", "1", debut, "AADADA"}},
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


        //[DataRow(Orientation.N, Orientation.O)]
        //[DataRow(Orientation.O, Orientation.S)]
        //[DataRow(Orientation.S, Orientation.E)]
        //[DataRow(Orientation.E, Orientation.N)]
        //[DataTestMethod]
        //public void VerifierTourAGauche(Orientation debut, Orientation fin)
        //{
        //    var aventurier = new Aventurier("Indiana", 1, 1, debut, "AADADA");
        //    aventurier.TournerAGauche();
        //    Assert.AreEqual(aventurier.Orientation, fin);
        //}

        [DataRow(Orientation.N, 2, 3, 2, 2)]
        [DataRow(Orientation.E, 1, 2, 2, 2)]
        [DataRow(Orientation.S, 1, 1, 1, 2)]
        [DataRow(Orientation.O, 1, 1, 0, 1)]
        [DataRow(Orientation.N, 1, 1, 1, 1)]
        [DataRow(Orientation.N, 0, 0, 0, 0)]
        [DataRow(Orientation.S, 2, 3, 2, 3)]
        [DataTestMethod]
        public void VerifierAvancee(Orientation orientation, int horizDep, int vertDep, int horizDest, int vertDest)
        {
            var aventurier = new Aventurier("Indiana", new Position(horizDep, vertDep), orientation, "AADADA");

            aventurier.Avancer(_carte);
            Assert.AreEqual(horizDest, aventurier.Position.AxeHorizontal);
            Assert.AreEqual(vertDest, aventurier.Position.AxeVertical);
        }

        //[DataTestMethod]
        //public void VerifierCollecteTresor()
        //{
        //    var aventurier = new Aventurier("Indiana", new Position(1, 3), Orientation.N, "AADADA");

        //    aventurier.Avancer(_carte);
        //    Assert.AreEqual(horizDest, aventurier.Position.AxeHorizontal);
        //    Assert.AreEqual(vertDest, aventurier.Position.AxeVertical);
        //}
    }
}
