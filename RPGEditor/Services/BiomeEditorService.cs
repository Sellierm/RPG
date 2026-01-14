using DataEntity;
using DataEntity.Interfaces;

namespace RPGEditor.Services
{
    public class BiomeEditorService
    {
        private readonly ICarteService _carteService;
        private readonly RPGContext _context;

        public BiomeEditorService(ICarteService carteService, RPGContext context)
        {
            _carteService = carteService;
            _context = context;
        }

        public void CreateBiome()
        {
            Console.WriteLine("\n=== Création d'un nouveau Biome ===");
            
            Console.Write("Nom du biome : ");
            string? nom = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nom))
            {
                Console.WriteLine("[!] Le nom ne peut pas être vide.");
                return;
            }

            Console.Write("Description : ");
            string? description = Console.ReadLine();

            Console.Write("Niveau requis pour y accéder : ");
            if (!int.TryParse(Console.ReadLine(), out int niveauRequis))
            {
                Console.WriteLine("[!] Niveau invalide.");
                return;
            }

            Console.Write("Niveau maximum des ennemis dans ce biome : ");
            if (!int.TryParse(Console.ReadLine(), out int niveauMaxEnnemi))
            {
                Console.WriteLine("[!] Niveau invalide.");
                return;
            }

            var carte = _context.CartesDbSet.FirstOrDefault();
            if (carte == null)
            {
                Console.WriteLine("[!] Aucune carte trouvée.");
                return;
            }

            var biome = new Biome
            {
                Nom = nom,
                Description = description ?? "",
                NiveauRequis = niveauRequis,
                NiveauMaxEnnemi = niveauMaxEnnemi,
                CarteId = carte.Id
            };

            _carteService.AjouterBiome(biome);
        }

        public void ListBiomes()
        {
            Console.WriteLine("\n=== Liste des Biomes ===");
            var biomes = _carteService.ListerBiomes();

            if (!biomes.Any())
            {
                Console.WriteLine("[!] Aucun biome trouvé.");
                return;
            }

            foreach (var biome in biomes)
            {
                Console.WriteLine($"\n[{biome.Id}] {biome.Nom} (Niveau {biome.NiveauRequis}+ | Ennemis max Niv.{biome.NiveauMaxEnnemi})");
                Console.WriteLine($"    Description: {biome.Description}");
                Console.WriteLine($"    Nombre de lieux: {biome.Lieux?.Count ?? 0}");
            }
        }

        public void UpdateBiome()
        {
            Console.WriteLine("\n=== Modification d'un Biome ===");
            ListBiomes();

            Console.Write("\nID du biome à modifier : ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("[!] ID invalide.");
                return;
            }

            var biome = _carteService.ObtenirBiome(id);
            if (biome == null)
            {
                Console.WriteLine("[!] Biome introuvable.");
                return;
            }

            Console.Write($"Nouveau nom [{biome.Nom}] : ");
            string? nom = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nom))
            {
                biome.Nom = nom;
            }

            Console.Write($"Nouvelle description [{biome.Description}] : ");
            string? description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                biome.Description = description;
            }

            Console.Write($"Nouveau niveau requis [{biome.NiveauRequis}] : ");
            string? niveauInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(niveauInput) && int.TryParse(niveauInput, out int niveauRequis))
            {
                biome.NiveauRequis = niveauRequis;
            }

            Console.Write($"Nouveau niveau max ennemis [{biome.NiveauMaxEnnemi}] : ");
            string? niveauMaxInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(niveauMaxInput) && int.TryParse(niveauMaxInput, out int niveauMaxEnnemi))
            {
                biome.NiveauMaxEnnemi = niveauMaxEnnemi;
            }

            _carteService.ModifierBiome(biome);
        }

        public void DeleteBiome()
        {
            Console.WriteLine("\n=== Suppression d'un Biome ===");
            ListBiomes();

            Console.Write("\nID du biome à supprimer : ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("[!] ID invalide.");
                return;
            }

            Console.Write("Confirmer la suppression ? (o/n) : ");
            string? confirmation = Console.ReadLine();
            
            if (confirmation?.ToLower() == "o")
            {
                _carteService.SupprimerBiome(id);
            }
            else
            {
                Console.WriteLine("[!] Suppression annulée.");
            }
        }
    }
}
