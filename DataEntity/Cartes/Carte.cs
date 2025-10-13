namespace DataEntity.Cartes
{
    public class Carte
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public List<Lieu> Lieux { get; set; } = new();
        public Carte(string nom, List<Lieu> lieux)
        {
            Nom = nom;
            Lieux = lieux;
        }
    }
}