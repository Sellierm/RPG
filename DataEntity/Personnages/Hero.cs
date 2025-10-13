using DataEntity.Objets;

namespace DataEntity.Personnages
{
    public class Hero : Personnage
    {
        public int Niveau { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public virtual List<Consommable> InventaireConsommable { get; set; } = new List<Consommable>();
        public virtual List<Arme> InventaireArme { get; set; } = new List<Arme>();
        //constructor
        public Hero(string nom, string description, int pointsDeVie, int force, int energie, int vitesse, int niveau, int experience, int gold)
        {
            Nom = nom;
            Description = description;
            PointsDeVie = pointsDeVie;
            Force = force;
            Energie = energie;
            Vitesse = vitesse;
            Niveau = niveau;
            Experience = experience;
            Gold = gold;
            InventaireConsommable = new List<Consommable>();
            InventaireArme = new List<Arme>();
        }
    }
}
