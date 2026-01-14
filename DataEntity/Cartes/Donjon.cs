namespace DataEntity
{
    // Représente un donjon dangereux avec des combats difficiles
    // Nécessite un niveau minimum pour y entrer
    public class Donjon : Batiment
    {
        public int NiveauRequis { get; set; }                // Niveau minimum pour entrer
        
        // Constructeur vide requis par Entity Framework
        public Donjon() { }
        
        // Constructeur pour créer un nouveau donjon
        public Donjon(string name, string description, int niveauRequis)
        {
            Nom = name;
            Description = description;
            NiveauRequis = niveauRequis;
        }
    }
}
