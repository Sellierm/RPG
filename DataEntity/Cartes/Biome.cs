namespace DataEntity
{
    // Représente une grande zone géographique (forêt, montagne, plaine, etc.)
    // Contient plusieurs lieux et a un niveau requis pour y accéder
    public class Biome
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NiveauRequis { get; set; }                // Niveau minimum du héros pour accéder
        public int NiveauMaxEnnemi { get; set; }             // Niveau maximum des ennemis générés
        public int CarteId { get; set; }
        public virtual Carte? Carte { get; set; }
        public virtual List<Lieu> Lieux { get; set; } = new();

        // Constructeur vide requis par Entity Framework
        public Biome() { }

        // Constructeur pour créer un nouveau biome
        public Biome(string nom, string description, int niveauRequis, int niveauMaxEnnemi, int carteId)
        {
            Nom = nom;
            Description = description;
            NiveauRequis = niveauRequis;
            NiveauMaxEnnemi = niveauMaxEnnemi;
            CarteId = carteId;
        }
    }
}
