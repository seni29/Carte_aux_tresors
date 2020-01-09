using System;

namespace CarteAuxTresors
{
    class Program
    {
        static void Main(string[] args)
        {
            var partie = new Partie(new FichierIO(@"C:\Users\Ines\Documents\in.txt"));
            partie.Initialiser();
            partie.Jouer();
            partie.Resultat();
        }

    }
  


}