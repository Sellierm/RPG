namespace DataEntity
{
    public class Magasin : Batiment
    {
        List<Consommable> Consommables { get; set; } = new();
    }
}
