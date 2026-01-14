namespace DataEntity
{
    // Représente une arme que le héros peut équiper
    // Ajoute des dégâts lors des combats
    public class Arme : Objet
    {
        public int Degats { get; set; }                      // Dégâts infligés en combat
        public string Type { get; set; } = string.Empty;     // Type d'arme (Épée, Hache, Arc, etc.)
    }
}
