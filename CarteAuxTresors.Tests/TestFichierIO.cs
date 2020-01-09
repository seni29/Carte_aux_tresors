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
            var fichier = new FichierIO(mockSystemeDeFichier);
            var lignes = fichier.RecupererDonnees(cheminFichier);

            //MockFileData mockOutputFile = mockFileSystem.GetFile(@"C:\temp\in.out.txt");

            //string[] outputLines = mockOutputFile.TextContents.SplitLines();

            Assert.AreEqual(5, lignes.Count);
            Assert.AreEqual("C - 3 - 4", lignes[0]);
            Assert.AreEqual("M - 2 - 1", lignes[1]);
            Assert.AreEqual("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésorsrestants}", lignes[2]);
            Assert.AreEqual("T - 1 - 3 - 2", lignes[3]);
            Assert.AreEqual("A - Lara - 0 - 3 - S - 3", lignes[4]);
            //Assert.AreEqual(TypeLigne.C, lignes[0]);
            //Assert.AreEqual("3", lignes[0].Contenu[0]);
            //Assert.AreEqual("4", lignes[0].Contenu[1]);
            //Assert.AreEqual(TypeLigne.M, lignes[1].Type);
            //Assert.AreEqual("2", lignes[1].Contenu[0]);
            //Assert.AreEqual("1", lignes[1].Contenu[1]);
            //Assert.AreEqual(TypeLigne.T, lignes[2].Type);
            //Assert.AreEqual("1", lignes[2].Contenu[0]);
            //Assert.AreEqual("3", lignes[2].Contenu[1]);
            //Assert.AreEqual("2", lignes[2].Contenu[2]);
            //Assert.AreEqual(TypeLigne.A, lignes[3].Type);
            //Assert.AreEqual("Lara", lignes[3].Contenu[0]);
            //Assert.AreEqual("0", lignes[3].Contenu[1]);
            //Assert.AreEqual("3", lignes[3].Contenu[2]);
            //Assert.AreEqual("S", lignes[3].Contenu[3]);
            //Assert.AreEqual("3", lignes[3].Contenu[4]);

        }

        [TestMethod]
        public void VerifierOutput()
        {
            var mockSystemeDeFichier = new MockFileSystem();
            var cheminFichier = @"C:\out.txt";
            var fichier = new FichierIO(mockSystemeDeFichier);
            //var lignes = new List<Ligne>
            //{
            //        new Ligne{Type=TypeLigne.C, Contenu=new List<string>{"3", "4"}},
            //        new Ligne{Type=TypeLigne.M, Contenu=new List<string>{"1", "0"}},
            //        new Ligne{Type=TypeLigne.T, Contenu=new List<string>{"0", "3", "2"}},
            //        new Ligne{Type=TypeLigne.A, Contenu=new List<string>{"Indiana", "1", "1", "S", "2"}}
            //};

            var lignes = new List<string>
              {
                  "C - 3 - 4",
                  "M - 1 - 0",
                  "T - 0 - 3 - 2",
                  "A - Indiana - 1 - 1 - S - 2"

              };

            fichier.Enregistrer(lignes, cheminFichier);

            MockFileData mockFichierSortie = mockSystemeDeFichier.GetFile(cheminFichier);
            var lignesSortie = mockFichierSortie.TextContents.SplitLines();

            Assert.AreEqual(4, lignesSortie.Length);
            Assert.AreEqual("C - 3 - 4", lignesSortie[0]);
            Assert.AreEqual("M - 1 - 0", lignesSortie[1]);
            Assert.AreEqual("T - 0 - 3 - 2", lignesSortie[2]);
            Assert.AreEqual("A - Indiana - 1 - 1 - S - 2", lignesSortie[3]);
            
        }

    }
}
