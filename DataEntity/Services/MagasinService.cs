using DataEntity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataEntity.Services
{
    public class MagasinService : IMagasinService
    {
        private readonly RPGContext _context;

        public MagasinService(RPGContext context)
        {
            _context = context;
        }

        // Affiche tous les objets disponibles à l'achat dans le magasin
        // Liste armes, armures et consommables avec leurs caractéristiques
        public void AfficherInventaire()
        {
            Console.WriteLine("\n=== MAGASIN ===");
            
            Console.WriteLine("\n--- Armes disponibles ---");
            var armes = GetArmesDisponibles();
            foreach (var arme in armes)
            {
                Console.WriteLine($"[{arme.Id}] {arme.Nom} - Dégâts: {arme.Degats} | Prix: {arme.Valeur} gold | Rareté: {arme.Rarete}");
            }

            Console.WriteLine("\n--- Armures disponibles ---");
            var armures = GetArmuresDisponibles();
            foreach (var armure in armures)
            {
                Console.WriteLine($"[{armure.Id}] {armure.Nom} - Défense: {armure.Defense} | Prix: {armure.Valeur} gold | Rareté: {armure.Rarete}");
            }

            Console.WriteLine("\n--- Consommables disponibles ---");
            var consommables = GetConsommablesDisponibles();
            foreach (var conso in consommables)
            {
                Console.WriteLine($"[{conso.Id}] {conso.Nom} - Effet: {conso.Effet} (+{conso.EffetValeur}) | Prix: {conso.Valeur} gold");
            }
        }

        // Permet au héros d'acheter une arme
        // Vérifie qu'il a assez d'or avant la transaction
        public void AcheterArme(Hero hero, Arme arme)
        {
            if (hero.Gold < arme.Valeur)
            {
                Console.WriteLine("Pas assez d'or !");
                return;
            }

            hero.Gold -= arme.Valeur;
            hero.InventaireArme.Add(arme);
            Console.WriteLine($"{arme.Nom} acheté ! Gold restant: {hero.Gold}");
        }

        // Permet au héros d'acheter une armure
        // Vérifie qu'il a assez d'or avant la transaction
        public void AcheterArmure(Hero hero, Armure armure)
        {
            if (hero.Gold < armure.Valeur)
            {
                Console.WriteLine("Pas assez d'or !");
                return;
            }

            hero.Gold -= armure.Valeur;
            hero.InventaireArmure.Add(armure);
            Console.WriteLine($"{armure.Nom} acheté ! Gold restant: {hero.Gold}");
        }

        // Permet au héros d'acheter un consommable
        // Crée une copie indépendante pour l'inventaire du héros
        public void AcheterConsommable(Hero hero, Consommable consommable)
        {
            if (hero.Gold < consommable.Valeur)
            {
                Console.WriteLine("Pas assez d'or !");
                return;
            }

            hero.Gold -= consommable.Valeur;
            
            // Créer une copie pour l'inventaire
            var copie = new Consommable(
                consommable.Nom,
                consommable.Valeur,
                consommable.IntelligenceRequise,
                consommable.NombreUtilisation,
                consommable.Effet,
                consommable.EffetValeur,
                consommable.Rarete
            );
            
            hero.InventaireConsommable.Add(copie);
            Console.WriteLine($"{consommable.Nom} acheté ! Gold restant: {hero.Gold}");
        }

        // Permet au héros de vendre un objet
        // Le prix de vente est la moitié du prix d'achat
        public void VendreObjet(Hero hero, Objet objet)
        {
            int prixVente = objet.Valeur / 2;
            hero.Gold += prixVente;
            Console.WriteLine($"{objet.Nom} vendu pour {prixVente} gold ! Total: {hero.Gold}");
        }

        // Récupère toutes les armes disponibles à l'achat
        public List<Arme> GetArmesDisponibles()
        {
            return _context.Set<Arme>().ToList();
        }

        // Récupère toutes les armures disponibles à l'achat
        public List<Armure> GetArmuresDisponibles()
        {
            return _context.Set<Armure>().ToList();
        }

        // Récupère tous les consommables disponibles à l'achat
        public List<Consommable> GetConsommablesDisponibles()
        {
            return _context.ConsommablesDbSet.ToList();
        }
    }
}
