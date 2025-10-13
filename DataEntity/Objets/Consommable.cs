using DataEntity.Enum;

namespace DataEntity.Objets
{
    public class Consommable : Objet
    {
        public int NombreUtilisation { get; set; }

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
