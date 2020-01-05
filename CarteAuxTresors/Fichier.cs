using System.Collections.Generic;

namespace CarteAuxTresors
{
    public class Fichier : IFournisseur
    {
        public IList<Ligne> RecupererDonnees()
        {
            return new List<Ligne>();
        }
    }
}