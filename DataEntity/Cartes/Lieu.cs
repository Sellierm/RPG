namespace DataEntity
{
    public class Lieu
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual Auberge? Auberge { get; set; }
        public virtual Forge? Forge { get; set; }
        public virtual Magasin? Magasin { get; set; }
        public virtual Mairie? Mairie { get; set; }
        public virtual Donjon? Donjon { get; set; }
        public Lieu(){}
        public Lieu(string nom, string description, Auberge? auberge = null, Forge? forge = null, Magasin? magasin = null, Mairie? mairie = null, Donjon? donjon = null)
        {
            Nom = nom;
            Description = description;
            Auberge = auberge;
            Forge = forge;
            Magasin = magasin;
            Mairie = mairie;
            Donjon = donjon;
        }
    }
}