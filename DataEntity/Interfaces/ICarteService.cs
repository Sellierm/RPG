namespace DataEntity.Interfaces
{
    public interface ICarteService
    {
        // Récupère la carte principale du jeu avec tous ses biomes et lieux
        Carte? ObtenirCarte();

        // ===== Gestion des Biomes =====
        
        // Ajoute un nouveau biome à la carte
        // Un biome est une grande zone comme une forêt ou une montagne
        void AjouterBiome(Biome biome);
        
        // Supprime un biome et tous ses lieux associés
        void SupprimerBiome(int id);
        
        // Modifie les informations d'un biome existant
        // Permet de changer le nom, la description ou le niveau requis
        void ModifierBiome(Biome biome);
        
        // Liste tous les biomes du jeu
        List<Biome> ListerBiomes();
        
        // Liste uniquement les biomes accessibles selon le niveau du héros
        // Filtre les zones trop difficiles
        List<Biome> ListerBiomesAccessibles(int niveauHero);
        
        // Récupère un biome spécifique avec tous ses détails
        Biome? ObtenirBiome(int id);

        // ===== Gestion des Lieux =====
        
        // Ajoute un nouveau lieu (village, ville, etc.) dans un biome
        void AjouterLieu(Lieu lieu, int biomeId);
        
        // Supprime un lieu et tous ses bâtiments
        void SupprimerLieu(int id);
        
        // Modifie les informations d'un lieu existant
        void ModifierLieu(Lieu lieu);
        
        // Liste tous les lieux du jeu
        List<Lieu> ListerLieux();
        
        // Liste les lieux d'un biome spécifique
        List<Lieu> ListerLieuxParBiome(int biomeId);
        
        // Récupère un lieu avec tous ses bâtiments
        Lieu? ObtenirLieu(int id);
        
        // ===== Gestion des Bâtiments =====
        
        // Ajoute un bâtiment (auberge, magasin, etc.) à un lieu
        void AjouterBatimentALieu(int lieuId, Batiment batiment);
        
        // Retire un bâtiment d'un lieu
        void SupprimerBatimentDeLieu(int lieuId, int batimentId);
    }
}
