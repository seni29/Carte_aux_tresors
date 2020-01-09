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
              new List<string>
              {
                  "C - 3 - 4",
                  "M - 1 - 0",
                  "# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésorsrestants}",
                  "T - 0 - 3 - 2",
                  "A - Indiana - 1 - 1 - S - AADADA"
                    
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
