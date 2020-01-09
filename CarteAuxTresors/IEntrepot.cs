using System.Collections.Generic;

namespace CarteAuxTresors
{
    public interface IEntrepot
    {
        IList<string> RecupererDonnees();
        void Enregistrer(IList<string> lignes);
    }
}