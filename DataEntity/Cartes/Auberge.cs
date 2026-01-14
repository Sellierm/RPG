using System.Runtime.CompilerServices;

namespace DataEntity
{
    // Représente une auberge où le héros peut se soigner et se reposer
    // Permet de restaurer PV et énergie contre de l'or
    public class Auberge : Batiment
    {
        // Constructeur vide requis par Entity Framework
        public Auberge() { }
        
        // Constructeur pour créer une nouvelle auberge
        public Auberge(string name, string description)
        {
            Nom = name;
            Description = description;
        }
    }
}
