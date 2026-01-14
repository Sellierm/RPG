namespace DataEntity
{
    // Représente la carte principale du monde du jeu
    // Contient tous les biomes et leurs lieux
    public class Carte
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual List<Biome> Biomes { get; set; } = new();

        // Constructeur vide requis par Entity Framework
        public Carte() { }

        // Constructeur pour créer une nouvelle carte
        public Carte(string nom, string description)
        {
            Nom = nom;
            Description = description;
        }
    }
}