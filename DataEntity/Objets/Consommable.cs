namespace DataEntity
{
    // Représente un objet consommable (potion, élixir, etc.)
    // Peut être utilisé pendant les combats pour des effets temporaires
    public class Consommable : Objet
    {
        public int NombreUtilisation { get; set; }           // Nombre de fois utilisable

        // Constructeur pour créer un nouveau consommable
        // Initialise toutes les caractéristiques du consommable
        public Consommable(string nom, int valeur, int intelligenceRequise, int nombreUtilisation, EffetType effet,int effetValeur, ERarete rarete)
        {
            Nom = nom;
            Valeur = valeur;
            IntelligenceRequise = intelligenceRequise;
            NombreUtilisation = nombreUtilisation;
            Effet = effet;
            EffetValeur = effetValeur;
            Rarete = rarete;
        }
    }
}
