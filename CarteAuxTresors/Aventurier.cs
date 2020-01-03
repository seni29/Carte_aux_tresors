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
        TourADroite = 'D',
        TourAGauche = 'G'
    }

    public class Aventurier
    {
        public Position Position { get; set; }
        private int _nbTresors;

        private string _nom;

        public Orientation Orientation { get; set; }
        public string Sequence { get; }

        public Aventurier(string nom, Position position, Orientation orientation, string sequence)
        {
            _nom = nom;
            Position = position;
            Orientation = orientation;
            Sequence = sequence;
        }


        public void JouerTour(Carte carte, int indice)
        {
            var action = (Action)Sequence[indice];
            switch (action)
            {
                case Action.Avancer:
                    Avancer(carte);
                    break;
                case Action.TourADroite:
                case Action.TourAGauche:
                    Tourner(action);
                    break;
            }
        }

        public void Avancer(Carte carte)
        {
            var nouvellePosition = CalculerNouvellePosition();
            try
            {
                var caseDestination = carte.Recuperer(nouvellePosition);
                if (caseDestination == null || caseDestination.EstObstacle)
                    return;

                var caseDepart = carte.Recuperer(Position);
                caseDepart.Liberer();
                Position = nouvellePosition;               
                caseDestination.Occuper();

                if (caseDestination is Tresor)
                    CollecterTresor((Tresor)caseDestination);

            }
            catch (IndexOutOfRangeException)
            {

            }

        }

        private void CollecterTresor(Tresor tresor)
        {
            tresor.Collecter(1);
            _nbTresors++;
        }

        private Position CalculerNouvellePosition()
        {
            var axeHorizontal = Position.AxeHorizontal;
            var axeVertical = Position.AxeVertical;
            switch (Orientation)
            {
                case Orientation.N:
                    axeVertical--;
                    break;
                case Orientation.S:
                    axeVertical++;
                    break;
                case Orientation.E:
                    axeHorizontal++;
                    break;
                case Orientation.O:
                    axeHorizontal--;
                    break;
            }

            return new Position(axeHorizontal, axeVertical);

        }

        public void Tourner(Action action)
        {
            switch (action)
            {
                case Action.TourADroite:
                    Orientation++;
                    break;
                case Action.TourAGauche:
                    Orientation--;
                    break;
            }

            if ((int)Orientation > 3)
                Orientation = 0;
            else if (Orientation < 0)
                Orientation = (Orientation)3;
        }



    }

}

public class Position
{
    public int AxeHorizontal { get; set; }
    public int AxeVertical { get; set; }

    public Position(int axeHorizontal, int axeVertical)
    {
        AxeHorizontal = axeHorizontal;
        AxeVertical = axeVertical;
    }


}



