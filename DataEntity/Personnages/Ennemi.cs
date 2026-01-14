namespace DataEntity
{
    // Représente un ennemi que le héros peut combattre
    // Hérite de Personnage pour avoir les caractéristiques de base
    public class Ennemi : Personnage
    {
        public int XpValeur { get; set; }        // Expérience gagnée en le battant
        public int GoldValeur { get; set; }      // Or gagné en le battant
        public ERace Race { get; set; }          // Race de l'ennemi (Orc, Gobelin, etc.)
    }
}
