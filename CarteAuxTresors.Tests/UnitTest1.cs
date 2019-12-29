using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
