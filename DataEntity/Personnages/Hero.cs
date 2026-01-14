namespace DataEntity
{
    // Représente un héros jouable avec ses stats, équipement et progression
    // Hérite de Personnage pour avoir les caractéristiques de base
    public class Hero : Personnage
    {
        public int Niveau { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int PointsCompetence { get; set; } // Points à répartir
        public int PvMax { get; set; } // Points de vie maximum
        public virtual Lieu? LieuActuel { get; set; }
        public virtual Batiment? BatimentActuel { get; set; }
        public virtual List<Consommable> InventaireConsommable { get; set; } = new List<Consommable>();
        public virtual List<Arme> InventaireArme { get; set; } = new List<Arme>();
        public virtual Arme? ArmeEquipee { get; set; }
        public virtual List<Armure> InventaireArmure { get; set; } = new List<Armure>();
        public virtual Armure? ArmureEquipee { get; set; }

        // Constructeur vide requis par Entity Framework
        public Hero() { }
        
        // Constructeur pour créer un nouveau héros
        // Initialise toutes les stats de base et les inventaires
        public Hero(string nom, string description, int pointsDeVie, int force, int energie, int vitesse, int niveau, int experience, int gold, Lieu? lieuActuel = null, Batiment? batimentActuel = null)
        {
            Nom = nom;
            Description = description;
            PointsDeVie = pointsDeVie;
            PvMax = 100; // Initialisation à 100 PV max
            Force = force;
            Energie = energie;
            Vitesse = vitesse;
            Niveau = niveau;
            Experience = experience;
            Gold = gold;
            PointsCompetence = 0;

            LieuActuel = lieuActuel;
            BatimentActuel = batimentActuel;
            InventaireConsommable = new List<Consommable>();
            InventaireArme = new List<Arme>();
        }
    }
}
