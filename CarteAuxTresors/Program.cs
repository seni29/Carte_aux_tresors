using System;
using System.Collections.Generic;
using System.Linq;

namespace CarteAuxTresors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var carte = new Carte();
            carte.Initialiser();
           
           


        }
        
    }

    public class Aventurier
    {
        public string Nom { get; }
        private int _axeHorizontal;
        private int _axeVertical;
        public Orientation Orientation { get; set; }
        public string Sequence { get; }


        public Aventurier(string nom, int axeHorizontal, int axeVertical, Orientation orientation, string sequence)
        {
            Nom = nom;
            _axeHorizontal = axeHorizontal;
            _axeVertical = axeVertical;
            Orientation = orientation;
            Sequence = sequence;
        }

        public void JouerTour(int indice)
        {
            var action = (Action)Sequence[indice];
            switch (action)
            {
                case Action.Avancer:
                    Avancer();
                    break;
                case Action.Droite:
                    TournerADroite();
                    break;
                case Action.Gauche:
                    TournerAGauche();
                    break;
            }
        }

        public void Avancer()
        {
            throw new NotImplementedException();
        }

        public void TournerADroite()
        {
            Orientation++;
            if ((int)Orientation > 3)
                Orientation = 0;

        }

        public void TournerAGauche()
        {
            Orientation--;
            if ((int)Orientation < 0)
                Orientation = (Orientation)3;
        }


    }


    class Carte
    {
        public int NombreDeTours { get {
                return Aventuriers.Select(x => x.Sequence).Select(y => y.Length).Max();
            }}
        IList<Aventurier> Aventuriers { get; set; }
        Case[,] Cases { get; set; }

        public void Initialiser()
        {
            Aventuriers = new List<Aventurier> { new Aventurier("Indiana", 1, 1, Orientation.S, "AADADA") };
            Cases = new Case[3,4];
            Cases[1, 0] = new Montagne();
            Cases[1, 2] = new Montagne();
            Cases[3, 0] = new Tresor(2);
            Cases[3, 1] = new Tresor(3);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                    if (Cases[i, j] == null)
                        Cases[i, j] = new Plaine();
        }

        public void Jouer()
        {
            for(int i=0;i< NombreDeTours; i++)
            {
                foreach(Aventurier aventurier in Aventuriers)
                {
                    aventurier.JouerTour(i);
                }
            }
            
        }


    }

    abstract class Case
    {

    }

    class Plaine : Case
    {

    }

    class Montagne : Case
    {

    }

    class Tresor : Case
    {
        private readonly int _nbTresors;

        public Tresor(int nbTresors)
        {
            _nbTresors = nbTresors;
        }
    }


    public enum Orientation
    {
        N=0, S=2, E=1, O=3 
    }

    enum Action { 
        Avancer= 'A', 
        Droite ='D', 
        Gauche ='G'}
}
