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

    public class Plaine : Case
    {

    }

    public class Montagne : Case
    {

    }


    public class Tresor : Case
    {
        private int _nbTresors;

        public Tresor(int nbTresors)
        {
            _nbTresors = nbTresors;
        }

        public void Collecter(int nbTresors)
        {
            _nbTresors -= nbTresors
        }
    }


}