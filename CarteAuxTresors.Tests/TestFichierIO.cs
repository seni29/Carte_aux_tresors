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
            var mockFileSystem = new MockFileSystem();
            var mockInputFile = new MockFileData("C - 3 - 4\nM - 2 - 1\n"
                + "# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésorsrestants}"
                + "\n T - 1 - 3 - 2\nA - Lara - 0 - 3 - S - 3");
;

            mockFileSystem.AddFile(@"C:\temp\in.txt", mockInputFile);
            var fichier = new FichierIO(mockFileSystem);
            var lignes= fichier.RecupererDonnees(@"C:\temp\in.txt");

            //MockFileData mockOutputFile = mockFileSystem.GetFile(@"C:\temp\in.out.txt");

            //string[] outputLines = mockOutputFile.TextContents.SplitLines();

            Assert.AreEqual(4, lignes.Count);
            Assert.AreEqual(TypeLigne.C, lignes[0].Type);
            Assert.AreEqual("3", lignes[0].ContenuCase[0]);
            Assert.AreEqual("4", lignes[0].ContenuCase[1]);
            Assert.AreEqual(TypeLigne.M, lignes[1].Type);
            Assert.AreEqual("2", lignes[1].ContenuCase[0]);
            Assert.AreEqual("1", lignes[1].ContenuCase[1]);
            Assert.AreEqual(TypeLigne.T, lignes[2].Type);
            Assert.AreEqual("1", lignes[2].ContenuCase[0]);
            Assert.AreEqual("3", lignes[2].ContenuCase[1]);
            Assert.AreEqual("2", lignes[2].ContenuCase[2]);
            Assert.AreEqual(TypeLigne.A, lignes[3].Type);
            Assert.AreEqual("Lara", lignes[3].ContenuCase[0]);
            Assert.AreEqual("0", lignes[3].ContenuCase[1]);
            Assert.AreEqual("3", lignes[3].ContenuCase[2]);
            Assert.AreEqual("S", lignes[3].ContenuCase[3]);
            Assert.AreEqual("3", lignes[3].ContenuCase[4]);
            ;
        }

        


    }
}
