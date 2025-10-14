namespace DataEntity
{
    public class Donjon : Batiment
    {
        public int NiveauRequis { get; set; }
        public Donjon() { }
        public Donjon(string name, string description, int niveauRequis)
        {
            Nom = name;
            Description = description;
            NiveauRequis = niveauRequis;
        }
    }
}
