namespace DataEntity
{
    public class Hero : Personnage
    {
        public int Niveau { get; set; }
        public int Experience { get; set; }
        public List<Consommable> InventaireConsommable { get; set; } = new List<Consommable>();
    }
}
