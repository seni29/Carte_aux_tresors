using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace CarteAuxTresors
{
    public enum TypeLigne { C, M, T, A }
    public class FichierIO : IEntrepot
    {
        private readonly IFileSystem _fileSystem;

        public FichierIO() : this(new FileSystem()) { }

        public FichierIO(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IList<string> RecupererDonnees(string source)
        {
            var lignes = new List<string>();
            using (StreamReader inputReader = _fileSystem.File.OpenText(source))
            {
                while (!inputReader.EndOfStream)
                {
                    lignes.Add(inputReader.ReadLine());
                }
            }

            return lignes;
            //using (StreamReader inputReader = _fileSystem.File.OpenText(source))
            //{
            //    while (!inputReader.EndOfStream)
            //    {
            //        var lines = inputReader.ReadLine().Split(" - ").ToList();
            //        try
            //        {
            //            var type = (TypeLigne)Enum.Parse(typeof(TypeLigne), lines[0]);
            //            lines.RemoveAt(0);
            //            lignes.Add(new Ligne
            //            {
            //                Type = type,
            //                Contenu = lines
            //            });
            //        }
            //        catch (Exception)
            //        {

            //        }
            //    }
            //}
            //return lignes;
        }
        public void Enregistrer(IList<string> lignes, string destination)
        {
            using (StreamWriter outputWriter = _fileSystem.File.CreateText(destination))
            {
                foreach(var ligne in lignes)
                {
                    outputWriter.WriteLine(ligne);
                }

            }
        }

    
    }

    }