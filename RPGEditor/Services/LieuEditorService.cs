using DataEntity;
using DataEntity.Interfaces;

namespace RPGEditor.Services
{
    public class LieuEditorService
    {
        private readonly ICarteService _carteService;
        private readonly RPGContext _context;

        public LieuEditorService(ICarteService carteService, RPGContext context)
        {
            _carteService = carteService;
            _context = context;
        }

        public void CreateLieu()
        {
            Console.WriteLine("\n=== Création d'un nouveau Lieu ===");
            
            // Sélectionner un biome
            var biomes = _carteService.ListerBiomes();
            if (!biomes.Any())
            {
                Console.WriteLine("[!] Aucun biome disponible. Créez d'abord un biome.");
                return;
            }

            Console.WriteLine("\nBiomes disponibles :");
            foreach (var b in biomes)
            {
                Console.WriteLine($"[{b.Id}] {b.Nom} (Niveau {b.NiveauRequis}+)");
            }

            Console.Write("ID du biome : ");
            if (!int.TryParse(Console.ReadLine(), out int biomeId))
            {
                Console.WriteLine("[!] ID invalide.");
                return;
            }

            Console.Write("Nom du lieu : ");
            string? nom = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nom))
            {
                Console.WriteLine("[!] Le nom ne peut pas être vide.");
                return;
            }

            Console.Write("Description : ");
            string? description = Console.ReadLine();

            var lieu = new Lieu
            {
                Nom = nom,
                Description = description ?? ""
            };

            _carteService.AjouterLieu(lieu, biomeId);
        }

        public void ListLieux()
        {
            Console.WriteLine("\n=== Liste des Lieux ===");
            var lieux = _carteService.ListerLieux();

            if (!lieux.Any())
            {
                Console.WriteLine("[!] Aucun lieu trouvé.");
                return;
            }

            foreach (var lieu in lieux)
            {
                Console.WriteLine($"\n[{lieu.Id}] {lieu.Nom}");
                Console.WriteLine($"    Description: {lieu.Description}");
                if (lieu.Biome != null)
                {
                    Console.WriteLine($"    Biome: {lieu.Biome.Nom} (Niveau {lieu.Biome.NiveauRequis}+)");
                }
                
                var batiments = new List<string>();
                if (lieu.Auberge != null) batiments.Add("Auberge");
                if (lieu.Magasin != null) batiments.Add("Magasin");
                if (lieu.Mairie != null) batiments.Add("Mairie");
                if (lieu.Donjon != null) batiments.Add("Donjon");
                
                if (batiments.Any())
                {
                    Console.WriteLine($"    Bâtiments: {string.Join(", ", batiments)}");
                }
            }
        }

        public void UpdateLieu()
        {
            Console.WriteLine("\n=== Modification d'un Lieu ===");
            ListLieux();

            Console.Write("\nID du lieu à modifier : ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("[!] ID invalide.");
                return;
            }

            var lieu = _carteService.ObtenirLieu(id);
            if (lieu == null)
            {
                Console.WriteLine("[!] Lieu introuvable.");
                return;
            }

            Console.Write($"Nouveau nom [{lieu.Nom}] : ");
            string? nom = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nom))
            {
                lieu.Nom = nom;
            }

            Console.Write($"Nouvelle description [{lieu.Description}] : ");
            string? description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                lieu.Description = description;
            }

            _carteService.ModifierLieu(lieu);
        }

        public void DeleteLieu()
        {
            Console.WriteLine("\n=== Suppression d'un Lieu ===");
            ListLieux();

            Console.Write("\nID du lieu à supprimer : ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("[!] ID invalide.");
                return;
            }

            Console.Write("Confirmer la suppression ? (o/n) : ");
            string? confirmation = Console.ReadLine();
            
            if (confirmation?.ToLower() == "o")
            {
                _carteService.SupprimerLieu(id);
            }
            else
            {
                Console.WriteLine("[!] Suppression annulée.");
            }
        }
    }
}
