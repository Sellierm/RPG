using DataEntity.Personnages;

namespace DataEntity.Cartes
{
    public class Lieu
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Auberge? Auberge { get; set; }
        public Forge? Forge { get; set; }
        public Magasin? Magasin { get; set; }
        public Mairie? Mairie { get; set; }
    }
}