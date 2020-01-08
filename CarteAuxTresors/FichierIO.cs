using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace CarteAuxTresors
{
    public class FichierIO : IEntrepot
    {
        private readonly IFileSystem _fileSystem;

        public FichierIO() : this(new FileSystem()) { }

        public FichierIO(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Enregistrer(IList<Ligne> lignes)
        {
            throw new System.NotImplementedException();
        }

        public IList<Ligne> RecupererDonnees(string fichierPath)
        {
            var lignes = new List<Ligne>();
            using (StreamReader inputReader = _fileSystem.File.OpenText(fichierPath))
            {
                while (!inputReader.EndOfStream)
                {
                    var lines = inputReader.ReadLine().Split(" - ").ToList();
                    try
                    {
                        var type = (TypeLigne)Enum.Parse(typeof(TypeLigne), lines[0]);
                        lines.RemoveAt(0);
                        lignes.Add(new Ligne
                        {
                            Type = type,
                            ContenuCase = lines
                        });
                    }
                    catch(Exception)
                    {

                    }
                }
            }
            return lignes;
        }
    }

    }