namespace DataEntity
{
    // Représente une armure que le héros peut équiper
    // Réduit les dégâts reçus lors des combats
    public class Armure : Objet
    {
        public int Defense { get; set; }                     // Points de défense
        
        // Constructeur vide requis par Entity Framework
        public Armure() { }
        
        // Constructeur pour créer une nouvelle armure
        // Initialise toutes les caractéristiques de l'armure
        public Armure(string nom, int valeur, int intelligenceRequise, int defense, string type, EffetType effet, int effetValeur, ERarete rarete)
        {
            Nom = nom;
            Valeur = valeur;
            IntelligenceRequise = intelligenceRequise;
            Defense = defense;
            Effet = effet;
            EffetValeur = effetValeur;
            Rarete = rarete;
        }
    }
}
