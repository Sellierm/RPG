using System.Runtime.CompilerServices;

namespace DataEntity
{
    public class Auberge : Batiment
    {
        public Auberge() { }
        public Auberge(string name, string description)
        {
            Nom = name;
            Description = description;
        }
    }
}
