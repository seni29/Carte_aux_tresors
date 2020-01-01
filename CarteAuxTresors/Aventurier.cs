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

    public class Aventurier : Case
    {
        private static Case[,] _carte;
        private int _nbTresors;

        public InformationsAventurier Informations { get; set; }


        public Aventurier(Case[,] carte, InformationsAventurier informations)
        {
            _carte = carte;
            Informations = informations;
        }

        public void JouerTour(int indice)
        {
            var action = (Action)Informations.Sequence[indice];
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
            //Informations.Deplacer(_carte);

            var nouvellesCoordonnees = CalculerCoordonneesDestination(Informations);
            try
            {
                var caseDestination = _carte[nouvellesCoordonnees.AxeHorizontal, nouvellesCoordonnees.AxeVertical];
                if (caseDestination.EstLibre)
                {
                    Informations.Coordonnees = nouvellesCoordonnees;
                    if (caseDestination is Tresor)         
                        CollecterTresor((Tresor)caseDestination);
                    
                }
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

        private Coordonnees CalculerCoordonneesDestination(InformationsAventurier informations)
        {
            var coordonnees = informations.Coordonnees;
            var axeHorizontal = coordonnees.AxeHorizontal;
            var axeVertical = coordonnees.AxeVertical;
            switch (informations.Orientation)
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

            return new Coordonnees(axeHorizontal, axeVertical);

        }

        public void Tourner(Action action)
        {
            Informations.ChangerOrientation(action);
        }



    }

    public class InformationsAventurier
    {
        private string _nom;

        public Coordonnees Coordonnees { get; set; }
        public Orientation Orientation { get; set; }
        public string Sequence { get; }

        public InformationsAventurier(string nom, Coordonnees coordonnees, Orientation orientation, string sequence)
        {
            _nom = nom;
            Coordonnees = coordonnees;
            Orientation = orientation;
            Sequence = sequence;
        }

        internal void ChangerOrientation(Action action)
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

public class Coordonnees
{
    public int AxeHorizontal { get; set; }
    public int AxeVertical { get; set; }

    public Coordonnees(int axeHorizontal, int axeVertical)
    {
        AxeHorizontal = axeHorizontal;
        AxeVertical = axeVertical;
    }


}
}


