using System.Collections.Generic;

namespace CarteAuxTresors
{
    public class Fichier : IEntrepot
    {
        public IList<Ligne> RecupererDonnees()
        {
            return new List<Ligne>();
        }
    }
}