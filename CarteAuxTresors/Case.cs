namespace CarteAuxTresors
{
    public abstract class Case
    {
        //public Position Position {get; set;}

        //public Case(Position position)
        //{
        //    Position = position;
        //}

        private bool _aventurier;
        public bool EstObstacle { get { return this is Montagne || _aventurier; } }

        public void DepartAventurier()
        {
            _aventurier = false;
        }

        public void ArriveeAventurier()
        {
            _aventurier = true;
        }
    }

    //public class Plaine : Case
    //{

    //}

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
            _nbTresors -= nbTresors;
        }
    }


}