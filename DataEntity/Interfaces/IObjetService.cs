namespace DataEntity.Interfaces
{
    public interface IObjetService
    {
        // ===== Gestion des Armes =====
        
        // Ajoute une nouvelle arme dans la base de données
        // L'arme sera disponible dans les magasins
        void AjouterArme(Arme arme);
        
        // Supprime une arme de la base
        void SupprimerArme(int id);
        
        // Modifie les caractéristiques d'une arme existante
        // Permet d'ajuster dégâts, prix, rareté, etc.
        void ModifierArme(Arme arme);
        
        // Liste toutes les armes disponibles dans le jeu
        List<Arme> ListerArmes();
        
        // Récupère une arme spécifique par son ID
        Arme? ObtenirArme(int id);

        // ===== Gestion des Armures =====
        
        // Ajoute une nouvelle armure dans la base de données
        void AjouterArmure(Armure armure);
        
        // Supprime une armure de la base
        void SupprimerArmure(int id);
        
        // Modifie les caractéristiques d'une armure existante
        // Permet d'ajuster défense, prix, rareté, etc.
        void ModifierArmure(Armure armure);
        
        // Liste toutes les armures disponibles dans le jeu
        List<Armure> ListerArmures();
        
        // Récupère une armure spécifique par son ID
        Armure? ObtenirArmure(int id);

        // ===== Gestion des Consommables =====
        
        // Ajoute un nouveau consommable (potion, élixir, etc.)
        void AjouterConsommable(Consommable consommable);
        
        // Supprime un consommable de la base
        void SupprimerConsommable(int id);
        
        // Modifie les caractéristiques d'un consommable existant
        void ModifierConsommable(Consommable consommable);
        
        // Liste tous les consommables disponibles dans le jeu
        List<Consommable> ListerConsommables();
        
        // Récupère un consommable spécifique par son ID
        Consommable? ObtenirConsommable(int id);
    }
}
