using System;

namespace CarteAuxTresors
{
    public enum Orientation
    {
        N = 0, E = 1, S = 2, O = 3
    }

    public enum Action
    {
        Avancer = 'A',
        Droite = 'D',
        Gauche = 'G'
    }

public class Aventurier : Case
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
}
