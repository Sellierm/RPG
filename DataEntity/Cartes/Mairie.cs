namespace DataEntity
{
    // Représente une mairie où le héros peut sauvegarder sa progression
    // Permet aussi de changer de personnage
    public class Mairie : Batiment
    {
        // Constructeur vide requis par Entity Framework
        public Mairie() { }
        
        // Constructeur pour créer une nouvelle mairie
        public Mairie(string nom, string description)
        {
            Nom = nom;
            Description = description;
        }
    }
}
