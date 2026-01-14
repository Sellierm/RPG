namespace DataEntity
{
    // Représente un lieu spécifique (village, ville, campement, etc.)
    // Peut contenir plusieurs bâtiments (auberge, magasin, etc.)
    public class Lieu
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BiomeId { get; set; }
        public virtual Biome? Biome { get; set; }
        public virtual Auberge? Auberge { get; set; }
        public virtual Forge? Forge { get; set; }
        public virtual Magasin? Magasin { get; set; }
        public virtual Mairie? Mairie { get; set; }
        public virtual Donjon? Donjon { get; set; }

        // Constructeur vide requis par Entity Framework
        public Lieu() { }

        // Constructeur pour créer un nouveau lieu avec ses bâtiments
        public Lieu(string nom, string description, int biomeId, Auberge? auberge = null, Forge? forge = null, Magasin? magasin = null, Mairie? mairie = null, Donjon? donjon = null)
        {
            Nom = nom;
            Description = description;
            BiomeId = biomeId;
            Auberge = auberge;
            Forge = forge;
            Magasin = magasin;
            Mairie = mairie;
            Donjon = donjon;
        }
    }
}