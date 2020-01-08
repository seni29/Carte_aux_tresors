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
                 new List<Ligne>
              {
                    new Ligne{Type=TypeLigne.Carte, ContenuCase=new List<string>{"3", "4"}},
                    new Ligne{Type=TypeLigne.Montagne, ContenuCase=new List<string>{"1", "0"}},
                    new Ligne{Type=TypeLigne.Tresor, ContenuCase=new List<string>{"1", "3", "3"}},
                    new Ligne{Type=TypeLigne.Aventurier, ContenuCase=new List<string>{"Indiana", "1", "1", "S", "AADADA"}},
                    new Ligne{Type=TypeLigne.Aventurier, ContenuCase=new List<string>{"Lara", "2", "3", "N", "AAGAG"}},
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
                 new List<Ligne>
              {
                    new Ligne{Type=TypeLigne.Carte, ContenuCase=new List<string>{"3", "4"}},
                    new Ligne{Type=TypeLigne.Montagne, ContenuCase=new List<string>{"1", "0"}},
                    new Ligne{Type=TypeLigne.Montagne, ContenuCase=new List<string>{"2", "1"}},
                    new Ligne{Type=TypeLigne.Tresor, ContenuCase=new List<string>{"0", "3", "2"}},
                    new Ligne{Type=TypeLigne.Tresor, ContenuCase=new List<string>{"1", "3", "3"}},
                    new Ligne{Type=TypeLigne.Aventurier, ContenuCase=new List<string>{"Indiana", "1", "1", "S", "AADADAGGA"}},
                    new Ligne{Type=TypeLigne.Aventurier, ContenuCase=new List<string>{"Lara", "0", "0", "S", "AAGAGAGAD"}},
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
            Assert.AreEqual(TypeLigne.Carte, resultat[0].Type);
            Assert.AreEqual("3", resultat[0].ContenuCase[0]);
            Assert.AreEqual("4", resultat[0].ContenuCase[1]);

            Assert.AreEqual(TypeLigne.Montagne, resultat[1].Type);
            Assert.AreEqual("1", resultat[1].ContenuCase[0]);
            Assert.AreEqual("0", resultat[1].ContenuCase[1]);

            Assert.AreEqual(TypeLigne.Montagne, resultat[2].Type);
            Assert.AreEqual("2", resultat[2].ContenuCase[0]);
            Assert.AreEqual("1", resultat[2].ContenuCase[1]);

            Assert.AreEqual(TypeLigne.Tresor, resultat[3].Type);
            Assert.AreEqual("1", resultat[3].ContenuCase[0]);
            Assert.AreEqual("3", resultat[3].ContenuCase[1]);
            Assert.AreEqual("2", resultat[3].ContenuCase[2]);

            Assert.AreEqual(TypeLigne.Aventurier, resultat[4].Type);
            Assert.AreEqual("Indiana", resultat[4].ContenuCase[0]);
            Assert.AreEqual("0", resultat[4].ContenuCase[1]);
            Assert.AreEqual("3", resultat[4].ContenuCase[2]);
            Assert.AreEqual("S", resultat[4].ContenuCase[3]);
            Assert.AreEqual("3", resultat[4].ContenuCase[4]);

            Assert.AreEqual(TypeLigne.Aventurier, resultat[5].Type);
            Assert.AreEqual("Lara", resultat[5].ContenuCase[0]);
            Assert.AreEqual("0", resultat[5].ContenuCase[1]);
            Assert.AreEqual("1", resultat[5].ContenuCase[2]);
            Assert.AreEqual("N", resultat[5].ContenuCase[3]);
            Assert.AreEqual("0", resultat[5].ContenuCase[4]);
        }


    }
}
