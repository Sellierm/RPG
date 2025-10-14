namespace DataEntity
{
    public class Carte
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual List<Lieu> Lieux { get; set; } = new();
        public Carte(){}
        public Carte(string nom,string description, List<Lieu> lieux)
        {
            Nom = nom;
            Description = description;
            Lieux = lieux;
        }
    }
}