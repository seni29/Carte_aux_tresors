namespace CarteAuxTresors
{
    public abstract class Case
    {
        //public Position Position {get; set;}

        //public Case(Position position)
        //{
        //    Position = position;
        //}

        private bool _libre =true;
        public bool EstObstacle { get { return this is Montagne || !_libre; } }

        public void Liberer()
        {
            _libre = true;
        }

        public void Occuper()
        {
            _libre = false;
        }
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
            _nbTresors -= nbTresors;
        }
    }


}