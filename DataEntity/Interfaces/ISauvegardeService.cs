namespace DataEntity.Interfaces
{
    public interface ISauvegardeService
    {
        // Sauvegarde la progression du héros dans un fichier JSON
        // Enregistre stats, équipement, inventaire et position
        void Sauvegarder(Hero hero);
        
        // Charge une sauvegarde existante depuis un fichier JSON
        // Reconstitue le héros avec toutes ses données
        Hero? Charger(string nomHero);
        
        // Charge toutes les sauvegardes disponibles
        // Permet de lister tous les héros sauvegardés
        List<Hero> ChargerTous();
        
        // Supprime définitivement une sauvegarde
        // Efface le fichier JSON du héros
        void Supprimer(string nomHero);
    }
}
