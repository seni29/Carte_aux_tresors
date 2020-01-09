using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;

namespace CarteAuxTresors.Tests
{
    [TestClass]
    public class TestFichierIO
    {
        [TestMethod]
        public void VerifierInput()
        {
            var mockSystemeDeFichier = new MockFileSystem();
            var mockFichierEntree = new MockFileData("C - 3 - 4\nM - 2 - 1\n"
                + "# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésorsrestants}"
                + "\nT - 1 - 3 - 2\nA - Lara - 0 - 3 - S - 3");

            var cheminFichier = @"C:\in.txt";
            mockSystemeDeFichier.AddFile(cheminFichier, mockFichierEntree);
            var fichier = new FichierIO(mockSystemeDeFichier, cheminFichier);
            var lignes = fichier.RecupererDonnees();

            Assert.AreEqual(5, lignes.Count);
            Assert.AreEqual("C - 3 - 4", lignes[0]);
            Assert.AreEqual("M - 2 - 1", lignes[1]);
            Assert.AreEqual("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésorsrestants}", lignes[2]);
            Assert.AreEqual("T - 1 - 3 - 2", lignes[3]);
            Assert.AreEqual("A - Lara - 0 - 3 - S - 3", lignes[4]);

        }

        [TestMethod]
        public void VerifierOutput()
        {
            var mockSystemeDeFichier = new MockFileSystem();
            var cheminFichier = @"C:\in.txt";
            var fichier = new FichierIO(mockSystemeDeFichier, cheminFichier);

            var lignes = new List<string>
              {
                  "C - 3 - 4",
                  "M - 1 - 0",
                  "T - 0 - 3 - 2",
                  "A - Indiana - 1 - 1 - S - 2"

              };

            fichier.Enregistrer(lignes);

            MockFileData mockFichierSortie = mockSystemeDeFichier.GetFile(@"C:\in.out.txt");
            var lignesSortie = mockFichierSortie.TextContents.SplitLines();

            Assert.AreEqual(4, lignesSortie.Length);
            Assert.AreEqual("C - 3 - 4", lignesSortie[0]);
            Assert.AreEqual("M - 1 - 0", lignesSortie[1]);
            Assert.AreEqual("T - 0 - 3 - 2", lignesSortie[2]);
            Assert.AreEqual("A - Indiana - 1 - 1 - S - 2", lignesSortie[3]);
            
        }

    }
}
