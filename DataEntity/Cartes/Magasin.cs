namespace DataEntity
{
    // Représente un magasin où le héros peut acheter et vendre des objets
    // Propose armes, armures et consommables
    public class Magasin : Batiment
    {
        public virtual List<Consommable> Consommables { get; set; } = new();
        
        // Constructeur vide requis par Entity Framework
        public Magasin() { }
        
        // Constructeur pour créer un nouveau magasin
        public Magasin(string nom, string description)
        {
            Nom = nom;
            Description = description;
        }
    }
}
