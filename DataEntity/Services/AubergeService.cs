using DataEntity.Interfaces;

namespace DataEntity.Services
{
    public class AubergeService : IAubergeService
    {
        private const int COUT_SOIN_COMPLET = 20;
        private const int COUT_REPOS = 10;

        // Soigne complètement le héros en restaurant tous ses PV
        // Coûte 20 pièces d'or
        public void Soigner(Hero hero)
        {
            if (hero.Gold < COUT_SOIN_COMPLET)
            {
                Console.WriteLine($"Pas assez d'or ! Il vous faut {COUT_SOIN_COMPLET} gold.");
                return;
            }

            hero.Gold -= COUT_SOIN_COMPLET;
            hero.PointsDeVie = hero.PvMax; // Soin complet aux PV max
            Console.WriteLine($"{hero.Nom} a été complètement soigné ! PV: {hero.PointsDeVie}/{hero.PvMax}");
        }

        // Fait se reposer le héros pour récupérer partiellement
        // Restaure 50 d'énergie et 25 PV pour 10 pièces d'or
        public void Reposer(Hero hero)
        {
            if (hero.Gold < COUT_REPOS)
            {
                Console.WriteLine($"Pas assez d'or ! Il vous faut {COUT_REPOS} gold.");
                return;
            }

            hero.Gold -= COUT_REPOS;
            hero.Energie = Math.Min(100, hero.Energie + 50);
            hero.PointsDeVie = Math.Min(hero.PvMax, hero.PointsDeVie + 25);
            Console.WriteLine($"{hero.Nom} se repose. Énergie: {hero.Energie}/100, PV: {hero.PointsDeVie}/{hero.PvMax}");
        }

        // Affiche le menu interactif de l'auberge
        // Montre les options disponibles et les stats du héros
        public void AfficherMenu(Hero hero)
        {
            Console.WriteLine("\n=== Auberge du Voyageur ===");
            Console.WriteLine($"Bienvenue {hero.Nom} !");
            Console.WriteLine($"Gold disponible: {hero.Gold}");
            Console.WriteLine($"PV actuels: {hero.PointsDeVie}/{hero.PvMax}");
            Console.WriteLine($"Énergie actuelle: {hero.Energie}/100");
            Console.WriteLine("\n1. Soin complet (20 gold)");
            Console.WriteLine("2. Se reposer (10 gold) - Restaure 50 énergie et 25 PV");
            Console.WriteLine("3. Quitter l'auberge");
            Console.Write("Choix : ");
        }
    }
}
