namespace DataEntity.Personnages
{
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
