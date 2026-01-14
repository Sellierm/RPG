namespace DataEntity.Interfaces
{
    public interface IAubergeService
    {
        // Soigne complètement le héros en échange d'or
        // Restaure tous les points de vie au maximum
        void Soigner(Hero hero);
        
        // Fait se reposer le héros pour récupérer énergie et PV partiellement
        // Coûte moins cher qu'un soin complet
        void Reposer(Hero hero);
        
        // Affiche le menu de l'auberge avec les options disponibles
        // Montre les stats actuelles du héros et les prix des services
        void AfficherMenu(Hero hero);
    }
}
