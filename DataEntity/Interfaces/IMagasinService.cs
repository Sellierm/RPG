namespace DataEntity.Interfaces
{
    public interface IMagasinService
    {
        // Affiche la liste complète des objets disponibles à l'achat
        // Montre armes, armures et consommables avec leurs prix
        void AfficherInventaire();
        
        // Permet au héros d'acheter une arme si il a assez d'or
        // L'arme est ajoutée à l'inventaire du héros
        void AcheterArme(Hero hero, Arme arme);
        
        // Permet au héros d'acheter une armure si il a assez d'or
        // L'armure est ajoutée à l'inventaire du héros
        void AcheterArmure(Hero hero, Armure armure);
        
        // Permet au héros d'acheter un consommable si il a assez d'or
        // Le consommable est ajouté à l'inventaire du héros
        void AcheterConsommable(Hero hero, Consommable consommable);
        
        // Permet au héros de vendre un objet pour récupérer de l'or
        // Le prix de vente est généralement la moitié du prix d'achat
        void VendreObjet(Hero hero, Objet objet);
        
        // Récupère la liste des armes disponibles à l'achat
        List<Arme> GetArmesDisponibles();
        
        // Récupère la liste des armures disponibles à l'achat
        List<Armure> GetArmuresDisponibles();
        
        // Récupère la liste des consommables disponibles à l'achat
        List<Consommable> GetConsommablesDisponibles();
    }
}
