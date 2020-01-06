using System.Collections.Generic;

namespace CarteAuxTresors
{
    public interface IEntrepot
    {
        IList<Ligne> RecupererDonnees();
    }
}