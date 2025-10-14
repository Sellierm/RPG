namespace DataEntity
{
    public class Forge : Batiment
    {
        public virtual List<Arme> ArmesDisponibles { get; set; } = new();
        public Forge() { }
        public Forge(string name, string description)
        {
            Nom = name;
            Description = description;
        }
    }
}
