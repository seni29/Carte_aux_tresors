﻿using System;

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
    //: Case
    {
        private Carte Carte { get { return Carte.Instance; } }
        public Position Position { get; set; }
        private int _nbTresors;

        private string _nom;

        public Orientation Orientation { get; set; }
        public string Sequence { get; }

        public Aventurier(string nom, Position position, Orientation orientation, string sequence)
        //: base(position)
        {
            _nom = nom;
            Position = position;
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
                case Action.TourADroite:
                case Action.TourAGauche:
                    Tourner(action);
                    break;
            }
        }

        public void Avancer()
        {
            var nouvellePosition = CalculerNouvellePosition();
            try
            {
                var caseDestination = Carte.Recuperer(nouvellePosition);
                if (caseDestination == null || caseDestination.EstObstacle)
                    return;

                var caseDepart = Carte.Recuperer(Position);
                caseDepart.DepartAventurier();
                Position = nouvellePosition;               
                caseDestination.ArriveeAventurier();

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

    public class InformationsAventurier
    {
        //private string _nom;

        //public Position Position { get; set; }
        //public Orientation Orientation { get; set; }
        //public string Sequence { get; }

        //public InformationsAventurier(string nom, Position coordonnees, Orientation orientation, string sequence)
        //{
        //    _nom = nom;
        //    Position = coordonnees;
        //    Orientation = orientation;
        //    Sequence = sequence;
        //}

        //internal void ChangerOrientation(Action action)
        //{
        //    switch (action)
        //    {
        //        case Action.TourADroite:
        //            Orientation++;
        //            break;
        //        case Action.TourAGauche:
        //            Orientation--;
        //            break;
        //    }

        //    if ((int)Orientation > 3)
        //        Orientation = 0;
        //    else if (Orientation < 0)
        //        Orientation = (Orientation)3;
        //}

        //public void Deplacer(Case[,] carte)
        //{
        //    var nouvellesCoordonnees = CalculerCoordonneesDestination();
        //    try
        //    {
        //        var caseDestination = carte[nouvellesCoordonnees.AxeHorizontal, nouvellesCoordonnees.AxeVertical];
        //        if (caseDestination.EstLibre)
        //        {
        //            _coordonnees = nouvellesCoordonnees;
        //            if(caseDestination)
        //        }
        //    }
        //    catch(IndexOutOfRangeException ex)
        //    {

        //    }

        //}


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



