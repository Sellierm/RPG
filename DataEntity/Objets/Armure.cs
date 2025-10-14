namespace DataEntity
{
    public class Armure : Objet
    {
        public int Defense { get; set; }
        public Armure() { }
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
