namespace CarteAuxTresors
{
    public abstract class Case
    {
        public bool EstLibre { get { return this is Plaine || this is Tresor} }
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