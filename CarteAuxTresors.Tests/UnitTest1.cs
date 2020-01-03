using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarteAuxTresors.Tests
{
    [TestClass]
    public class UnitTest1
    {
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

        [DataRow(Orientation.N, 1, 1)]
        [DataRow(Orientation.E, 1, 1)]
        [DataRow(Orientation.S, 1, 2)]
        [DataRow(Orientation.S, 0, 1)]
        [DataTestMethod]
        public void VerifierAvancee(Orientation orientation,  int horizDest, int vertDest)
        {
            Carte.Instance.Initialiser(
               new List<Ligne>
               {
                    new Ligne{Type='C', ContenuCase=new List<string>{"3", "4"}},
                    new Ligne{Type='M', ContenuCase=new List<string>{"1", "0"}},
                    new Ligne{Type='M', ContenuCase=new List<string>{"2", "1"}},
                    new Ligne{Type='T', ContenuCase=new List<string>{"0", "3", "2"}},
                    new Ligne{Type='T', ContenuCase=new List<string>{"1", "3", "3"}},
                   //new Ligne{Type='A', ContenuCase=new List<string>{"Indiana", "1", "1", debut, "AADADA"}},
               });

            var aventurier = new Aventurier("Indiana", new Position(1, 1), orientation, "AADADA");

           
            aventurier.Avancer();
            Assert.AreEqual(aventurier.Position.AxeHorizontal, horizDest);
            Assert.AreEqual(aventurier.Position.AxeVertical, vertDest);
        }
    }
}
