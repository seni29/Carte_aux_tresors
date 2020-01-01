using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarteAuxTresors.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [DataRow(Orientation.N, Orientation.E)]
        [DataRow(Orientation.E, Orientation.S)]
        [DataRow(Orientation.S, Orientation.O)]
        [DataRow(Orientation.O, Orientation.N)]
        [DataTestMethod]
        public void VerifierTourADroite( Orientation debut, Orientation fin)
        {
            var aventurier = new Aventurier("Indiana", 1, 1, debut, "AADADA");
            aventurier.TournerADroite();
            Assert.AreEqual(aventurier.Orientation, fin);
        }


        [DataRow(Orientation.N, Orientation.O)]
        [DataRow(Orientation.O, Orientation.S)]
        [DataRow(Orientation.S, Orientation.E)]
        [DataRow(Orientation.E, Orientation.N)]
        [DataTestMethod]
        public void VerifierTourAGauche(Orientation debut, Orientation fin)
        {
            var aventurier = new Aventurier("Indiana", 1, 1, debut, "AADADA");
            aventurier.TournerAGauche();
            Assert.AreEqual(aventurier.Orientation, fin);
        }

        [DataRow("S", Orientation.N)]
        [DataTestMethod]
        public void VerifierAvancee(string debut, Orientation fin)
        {
            var aventurier = new Aventurier("Indiana", 1, 1, debut, "AADADA");

            Carte.Instance.Initialiser(
                new List<Ligne>
                {
                    new Ligne{TypeCase='C', ContenuCase=new List<string>{"3", "4"}},
                    new Ligne{TypeCase='M', ContenuCase=new List<string>{"1", "0"}},
                    new Ligne{TypeCase='M', ContenuCase=new List<string>{"1", "2"}},
                    new Ligne{TypeCase='T', ContenuCase=new List<string>{"3", "0", "2"}},
                    new Ligne{TypeCase='T', ContenuCase=new List<string>{"3", "1", "3"}},
                    new Ligne{TypeCase='A', ContenuCase=new List<string>{"Indiana", "1", "1", debut, "AADADA"}},
                });
            aventurier.Avancer();
            Assert.AreEqual(aventurier.Orientation, fin);
        }
    }
}
