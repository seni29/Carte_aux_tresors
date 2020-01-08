using System.Collections.Generic;

namespace CarteAuxTresors
{
    public interface IEntrepot
    {
        IList<Ligne> RecupererDonnees(string fichierPath);
        void Enregistrer(IList<Ligne> lignes);
    }
}