using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CarteAuxTresors.Tests
{
    [TestClass]
    public class TestCarte
    {
        [TestMethod]
        public void VerifierInitialisation()
        {
            var carte = Carte.Instance.Initialiser(
              new List<Ligne>
              {
                    new Ligne{Type=TypeLigne.Carte, ContenuCase=new List<string>{"3", "4"}},
                    new Ligne{Type=TypeLigne.Montagne, ContenuCase=new List<string>{"1", "0"}},
                    new Ligne{Type=TypeLigne.Tresor, ContenuCase=new List<string>{"0", "3", "2"}},
                    new Ligne{Type=TypeLigne.Aventurier, ContenuCase=new List<string>{"Indiana", "1", "1", "S", "AADADA"}},
              });

            var cases = carte.Cases;
            Assert.AreEqual(3, cases.GetLength(0));
            Assert.AreEqual(4, cases.GetLength(1));
            Assert.AreEqual(typeof(Montagne), cases[1, 0].GetType());
            Assert.IsTrue(cases[1, 0].EstLibre);
            Assert.AreEqual(typeof(Tresor), cases[0, 3].GetType());
            Assert.AreEqual(2, ((Tresor)cases[0, 3]).NbTresors);
            Assert.IsTrue(cases[2, 1].EstLibre);
            Assert.AreEqual(typeof(Plaine), cases[1, 1].GetType());
            Assert.IsFalse(cases[1, 1].EstLibre);
        }


    }
}
