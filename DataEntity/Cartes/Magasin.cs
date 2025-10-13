using DataEntity.Objets;

namespace DataEntity.Cartes
{
    public class Magasin : Batiment
    {
        List<Consommable> Consommables { get; set; } = new();
    }
}
