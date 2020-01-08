using System;
using System.Collections.Generic;

namespace CarteAuxTresors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var partie = new Partie(new FichierIO());
            partie.Initialiser();

            //var carte = new Carte();
            //carte.Initialiser();




        }

    }


    public enum TypeLigne { C, M, T, A}
    public class Ligne
    {
        public TypeLigne Type { get; set; }
        public List<string> ContenuCase { get; set; }
    }

    


}