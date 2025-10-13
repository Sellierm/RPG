namespace DataEntity
{
    public class Arme : Object
    {
        public int Degats { get; set; }
        public string Type { get; set; } = string.Empty; // e.g., "Melee", "Ranged"
    }
}
