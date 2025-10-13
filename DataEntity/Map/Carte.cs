namespace DataEntity.Map
{
    public class Carte
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public List<Lieu> Lieux { get; set; } = new();
    }
}