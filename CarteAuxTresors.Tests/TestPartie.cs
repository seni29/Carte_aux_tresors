using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CarteAuxTresors.Tests
{
    [TestClass]
    public class TestPartie
    {

        [TestMethod]
        public void VerifierInitialisation()
        {
            var entrepot = Mock.Of<IEntrepot>();
            Mock.Get(entrepot).Setup(x => x.RecupererDonnees()).Returns(
            new List<string>
              {
                  "C - 3 - 4",
                  "M - 1 - 0",
                  "# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésorsrestants}",
                  "T - 1 - 3 - 3",
                  "A - Indiana - 1 - 1 - S - AADADA",
                  "A - Lara - 2 - 3 - N - AAGAG"

              });

            var partie = new Partie(entrepot).Initialiser();
            var cases = partie.Carte.Cases;
            Assert.AreEqual(3, cases.GetLength(0));
            Assert.AreEqual(4, cases.GetLength(1));

            Assert.AreEqual(2, partie.Aventuriers.Count);
            var aventurier1 = partie.Aventuriers[0];
            var aventurier2 = partie.Aventuriers[1];
            Assert.AreEqual("Indiana", aventurier1.Nom);
            Assert.AreEqual(1, aventurier1.Position.AxeHorizontal);
            Assert.AreEqual(1, aventurier1.Position.AxeVertical);
            Assert.AreEqual(Orientation.S, aventurier1.Orientation);
            Assert.AreEqual("AADADA", aventurier1.Sequence);

            Assert.AreEqual("Lara", aventurier2.Nom);
            Assert.AreEqual(2, aventurier2.Position.AxeHorizontal);
            Assert.AreEqual(3, aventurier2.Position.AxeVertical);
            Assert.AreEqual(Orientation.N, aventurier2.Orientation);
            Assert.AreEqual("AAGAG", aventurier2.Sequence);


            Assert.AreEqual(5, partie.NombreDeTours);
        }

        [TestMethod]
        public void VerifierJeu()
        {
            var entrepot = Mock.Of<IEntrepot>();
            Mock.Get(entrepot).Setup(x => x.RecupererDonnees()).Returns(
            new List<string>
              {
                  "C - 3 - 4",
                  "M - 1 - 0",
                  "M - 2 - 1",
                  "T - 0 - 3 - 2",
                  "T - 1 - 3 - 3",
                  "A - Indiana - 1 - 1 - S - AADADAGGA",
                  "A - Lara - 0 - 0 - S - AAGAGAGAD"

              });

            var partie = new Partie(entrepot).Initialiser();
            partie.Jouer();

            var aventurier1 = partie.Aventuriers[0];
            Assert.AreEqual(0, aventurier1.Position.AxeHorizontal);
            Assert.AreEqual(3, aventurier1.Position.AxeVertical);
            Assert.AreEqual(Orientation.S, aventurier1.Orientation);
            Assert.AreEqual(3, aventurier1.NbTresors);

            var aventurier2 = partie.Aventuriers[1];
            Assert.AreEqual(0, aventurier2.Position.AxeHorizontal);
            Assert.AreEqual(1, aventurier2.Position.AxeVertical);
            Assert.AreEqual(Orientation.N, aventurier2.Orientation);
            Assert.AreEqual(0, aventurier2.NbTresors);

            var carte = partie.Carte;
            Assert.IsFalse(carte.Cases[0, 3].EstLibre);
            Assert.IsFalse(carte.Cases[0, 1].EstLibre);
            Assert.AreEqual(0, ((Tresor)carte.Cases[0, 3]).NbTresors);
            Assert.AreEqual(2, ((Tresor)carte.Cases[1, 3]).NbTresors);

            var resultat = partie.Resultat();

            Assert.AreEqual(6, resultat.Count);
            Assert.AreEqual("C - 3 - 4", resultat[0]);
            Assert.AreEqual("M - 1 - 0", resultat[1]);
            Assert.AreEqual("M - 2 - 1", resultat[2]);
            Assert.AreEqual("T - 1 - 3 - 2", resultat[3]);
            Assert.AreEqual("A - Indiana - 0 - 3 - S - 3", resultat[4]);
            Assert.AreEqual("A - Lara - 0 - 1 - N - 0", resultat[5]);
        }


    }
}
