using DataEntity.Cartes;
using DataEntity.Objets;

namespace DataEntity.Personnages
{
    public class Forge : Batiment
    {
        public List<Arme> ArmesDisponibles { get; set; } = new();
    }
}
