using System.Collections.Generic;

namespace CarteAuxTresors
{
    public interface IEntrepot
    {
        IList<string> RecupererDonnees(string source);
        void Enregistrer(IList<string> lignes, string destination);
    }
}