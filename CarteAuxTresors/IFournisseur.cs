using System.Collections.Generic;

namespace CarteAuxTresors
{
    public interface IFournisseur
    {
        IList<Ligne> RecupererDonnees();
    }
}