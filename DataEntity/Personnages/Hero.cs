namespace DataEntity
{
    public class Hero : Personnage
    {
        public int Niveau { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public virtual Lieu? LieuActuel { get; set; }
        public virtual Batiment? BatimentActuel { get; set; }
        public virtual List<Consommable> InventaireConsommable { get; set; } = new List<Consommable>();
        public virtual List<Arme> InventaireArme { get; set; } = new List<Arme>();
        public virtual Arme? ArmeEquipee { get; set; }
        public virtual List<Armure> InventaireArmure { get; set; } = new List<Armure>();
        public virtual Armure? ArmureEquipee { get; set; }

        //constructor,
        public Hero() { }
        public Hero(string nom, string description, int pointsDeVie, int force, int energie, int vitesse, int niveau, int experience, int gold, Lieu? lieuActuel = null, Batiment? batimentActuel = null)
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

            LieuActuel = lieuActuel;
            BatimentActuel = batimentActuel;
            InventaireConsommable = new List<Consommable>();
            InventaireArme = new List<Arme>();
        }
    }
}
