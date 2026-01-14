namespace DataEntity.Interfaces
{
    public interface IEnnemiService
    {
        // ===== Gestion des Ennemis =====
        
        // Ajoute un nouveau type d'ennemi dans la base de données
        // Sert de template pour générer des ennemis dans le jeu
        void AjouterEnnemi(Ennemi ennemi);
        
        // Supprime un type d'ennemi de la base
        void SupprimerEnnemi(int id);
        
        // Modifie les caractéristiques d'un ennemi existant
        // Permet d'ajuster la difficulté ou les récompenses
        void ModifierEnnemi(Ennemi ennemi);
        
        // Liste tous les types d'ennemis disponibles
        List<Ennemi> ListerEnnemis();
        
        // Récupère un ennemi spécifique par son ID
        Ennemi? ObtenirEnnemi(int id);
        
        // Génère un ennemi adapté à un niveau donné
        // Prend un template aléatoire et ajuste ses stats
        Ennemi GenererEnnemiParNiveau(int niveau);
        
        // Génère un ennemi pour un biome spécifique
        // Le niveau de l'ennemi est limité par le niveau max du biome
        Ennemi GenererEnnemiPourBiome(int niveauHero, int niveauMaxBiome);
    }
}
