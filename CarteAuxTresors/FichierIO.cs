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
        private readonly string _source;

        public FichierIO(string source) : this(new FileSystem(), source) { }

        public FichierIO(IFileSystem fileSystem, string source)
        {
            _fileSystem = fileSystem;
            _source = source;
        }

        public IList<string> RecupererDonnees()
        {
            var lignes = new List<string>();
            using (StreamReader inputReader = _fileSystem.File.OpenText(_source))
            {
                while (!inputReader.EndOfStream)
                {
                    lignes.Add(inputReader.ReadLine());
                }
            }

            return lignes;
        }
        public void Enregistrer(IList<string> lignes)
        {
            string fichierSortie = Path.ChangeExtension(_source, ".out.txt");
            using (StreamWriter outputWriter = _fileSystem.File.CreateText(fichierSortie))
            {
                foreach(var ligne in lignes)
                {
                    outputWriter.WriteLine(ligne);
                }

            }
        }

    
    }

    }