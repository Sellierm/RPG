namespace DataEntity
{
    // Classe abstraite de base pour tous les personnages (héros et ennemis)
    // Contient les caractéristiques communes comme PV, Force, etc.
    public abstract class Personnage
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PointsDeVie { get; set; }
        public int Force { get; set; }
        public int Energie { get; set; }
        public int Vitesse { get; set; }
    }
}
