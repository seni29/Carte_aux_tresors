using System;
using System.Collections.Generic;

namespace CarteAuxTresors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var partie = new Partie();
            //partie.Initialiser();

            //var carte = new Carte();
            //carte.Initialiser();




        }

    }

    public class Ligne
    {
        public Char TypeCase { get; set; }
        public List<string> ContenuCase { get; set; }
    }

    


}