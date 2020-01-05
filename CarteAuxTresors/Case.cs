namespace CarteAuxTresors
{
    public abstract class Case
    {
        public bool EstObstacle { get { return this is Montagne || !EstLibre; } }

        public bool EstLibre { get; set; } = true;

        public void Liberer()
        {
            EstLibre = true;
        }

        public void Occuper()
        {
            EstLibre = false;
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
        public Tresor(int nbTresors)
        {
            NbTresors = nbTresors;
        }

        public int NbTresors { get; set; }

        public void Collecter(int nbTresors)
        {
            NbTresors -= nbTresors;
        }
    }


}