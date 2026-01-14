namespace DataEntity
{
    // Classe abstraite de base pour tous les bâtiments du jeu
    // Auberges, magasins, donjons, etc. héritent de cette classe
    public abstract class Batiment
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
} 