namespace DataEntity
{
    // Représente une forge où le héros peut acheter et améliorer des armes
    // Spécialisée dans l'équipement offensif
    public class Forge : Batiment
    {
        public virtual List<Arme> ArmesDisponibles { get; set; } = new();
        
        // Constructeur vide requis par Entity Framework
        public Forge() { }
        
        // Constructeur pour créer une nouvelle forge
        public Forge(string name, string description)
        {
            Nom = name;
            Description = description;
        }
    }
}
