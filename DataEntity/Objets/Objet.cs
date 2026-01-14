namespace DataEntity
{
    // Classe abstraite de base pour tous les objets du jeu
    // Armes, armures et consommables héritent de cette classe
    public abstract class Objet
    {
       public int Id { get; set; }
       public string Nom { get; set; } = string.Empty;
       public int Valeur { get; set; }                       // Prix d'achat/vente
       public int IntelligenceRequise { get; set; }          // Niveau d'intelligence requis
       public EffetType Effet { get; set; }                  // Type d'effet bonus
       public int EffetValeur { get; set; }                  // Valeur de l'effet
       public ERarete Rarete { get; set; }                   // Rareté de l'objet
    }
}
