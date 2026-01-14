namespace DataEntity
{
    // Énumération des différents types d'effets possibles pour les objets
    // Utilisé pour les armes, armures et consommables
    public enum EffetType
    {
        Soin,              // Restaure des points de vie
        Degats,            // Inflige des dégâts supplémentaires
        BuffForce,         // Augmente temporairement la force
        BuffEnergie,       // Augmente l'énergie
        BuffIntelligence,  // Augmente l'intelligence
        Debuff,            // Applique un effet négatif
        Protection,        // Augmente la défense
        Vitesse,           // Augmente la vitesse
        Poison,            // Applique un poison
        Rien,              // Aucun effet (obsolète)
        Force,             // Augmentation temporaire de force
        Aucun              // Pas d'effet
    }
}