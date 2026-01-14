namespace DataEntity.Interfaces
{
    public interface ICombatService
    {
        // Lance un combat entre le héros et un ennemi
        // Retourne true si le héros est mort, false sinon
        bool CommencerCombat(Hero hero, Ennemi ennemi);
        
        // Fait attaquer un personnage par un autre
        // Calcule les dégâts en fonction de la force et de l'équipement
        // Retourne true si le défenseur est mort
        bool Attaquer(Personnage attaquant, Personnage defenseur);
        
        // Fait gagner de l'expérience au héros
        // Vérifie automatiquement si le héros monte de niveau
        void GagnerExperience(Hero hero, int xp);
        
        // Vérifie si le héros a assez d'XP pour passer au niveau suivant
        // Gère la montée de niveau et la distribution des points de compétence
        void VerifierMonteeNiveau(Hero hero);
        
        // Génère un ennemi aléatoire adapté au niveau du héros
        // Choisit un template d'ennemi et ajuste ses stats selon le niveau
        Ennemi GenererEnnemiAleatoire(int niveauHero);
    }
}
