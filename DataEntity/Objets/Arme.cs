namespace DataEntity.Objets
{
    public class Arme : Objet
    {
        public int Degats { get; set; }
        public string Type { get; set; } = string.Empty; // e.g., "Melee", "Ranged"
    }
}
