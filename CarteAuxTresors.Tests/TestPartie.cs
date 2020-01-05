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
            var entrepot = Mock.Of<IFournisseur>();
            Mock.Get(entrepot).Setup(x => x.RecupererDonnees()).Returns(
                 new List<Ligne>
              {
                    new Ligne{Type=TypeLigne.Carte, ContenuCase=new List<string>{"3", "4"}},
                    new Ligne{Type=TypeLigne.Montagne, ContenuCase=new List<string>{"1", "0"}},
                    new Ligne{Type=TypeLigne.Tresor, ContenuCase=new List<string>{"1", "3", "3"}},
                    new Ligne{Type=TypeLigne.Aventurier, ContenuCase=new List<string>{"Indiana", "1", "1", "S", "AADADA"}},
                    new Ligne{Type=TypeLigne.Aventurier, ContenuCase=new List<string>{"Luca", "0", "1", "S", "AADADA"}},
              });

            var partie = new Partie(entrepot).Initialiser();

            var cases = partie.Carte.Cases;
            Assert.AreEqual(3, cases.GetLength(0));
            Assert.AreEqual(4, cases.GetLength(1));

            Assert.AreEqual(2, partie.Aventuriers.Count);

            
        }


    }
}
