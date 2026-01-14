using DataEntity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataEntity.Services
{
    public class CarteService : ICarteService
    {
        private readonly RPGContext _context;

        public CarteService(RPGContext context)
        {
            _context = context;
        }

        // Carte (une seule)
        public Carte? ObtenirCarte()
        {
            return _context.CartesDbSet
                .Include(c => c.Biomes)
                    .ThenInclude(b => b.Lieux)
                        .ThenInclude(l => l.Auberge)
                .Include(c => c.Biomes)
                    .ThenInclude(b => b.Lieux)
                        .ThenInclude(l => l.Magasin)
                .Include(c => c.Biomes)
                    .ThenInclude(b => b.Lieux)
                        .ThenInclude(l => l.Mairie)
                .Include(c => c.Biomes)
                    .ThenInclude(b => b.Lieux)
                        .ThenInclude(l => l.Donjon)
                .FirstOrDefault();
        }

        // CRUD pour Biomes
        public void AjouterBiome(Biome biome)
        {
            var carte = _context.CartesDbSet.Include(c => c.Biomes).FirstOrDefault();
            if (carte != null)
            {
                biome.CarteId = carte.Id;
                _context.BiomesDbSet.Add(biome);
                _context.SaveChanges();
                Console.WriteLine($"[OK] Biome '{biome.Nom}' ajouté avec succès !");
            }
            else
            {
                Console.WriteLine("[!] Aucune carte trouvée. Créez d'abord une carte.");
            }
        }

        public void SupprimerBiome(int id)
        {
            var biome = _context.BiomesDbSet.Find(id);
            if (biome != null)
            {
                _context.BiomesDbSet.Remove(biome);
                _context.SaveChanges();
                Console.WriteLine($"[OK] Biome '{biome.Nom}' supprimé !");
            }
            else
            {
                Console.WriteLine("[!] Biome introuvable.");
            }
        }

        public void ModifierBiome(Biome biome)
        {
            var biomeExistant = _context.BiomesDbSet.Find(biome.Id);
            if (biomeExistant != null)
            {
                biomeExistant.Nom = biome.Nom;
                biomeExistant.Description = biome.Description;
                biomeExistant.NiveauRequis = biome.NiveauRequis;
                _context.SaveChanges();
                Console.WriteLine($"[OK] Biome '{biome.Nom}' modifié !");
            }
            else
            {
                Console.WriteLine("[!] Biome introuvable.");
            }
        }

        public List<Biome> ListerBiomes()
        {
            return _context.BiomesDbSet
                .Include(b => b.Lieux)
                .OrderBy(b => b.NiveauRequis)
                .ToList();
        }

        public List<Biome> ListerBiomesAccessibles(int niveauHero)
        {
            return _context.BiomesDbSet
                .Include(b => b.Lieux)
                .Where(b => b.NiveauRequis <= niveauHero)
                .OrderBy(b => b.NiveauRequis)
                .ToList();
        }

        public Biome? ObtenirBiome(int id)
        {
            return _context.BiomesDbSet
                .Include(b => b.Lieux)
                    .ThenInclude(l => l.Auberge)
                .Include(b => b.Lieux)
                    .ThenInclude(l => l.Magasin)
                .Include(b => b.Lieux)
                    .ThenInclude(l => l.Mairie)
                .Include(b => b.Lieux)
                    .ThenInclude(l => l.Donjon)
                .FirstOrDefault(b => b.Id == id);
        }

        // CRUD pour Lieux
        public void AjouterLieu(Lieu lieu, int biomeId)
        {
            var biome = _context.BiomesDbSet.Include(b => b.Lieux).FirstOrDefault(b => b.Id == biomeId);
            if (biome != null)
            {
                lieu.BiomeId = biomeId;
                _context.LieuxDbSet.Add(lieu);
                _context.SaveChanges();
                Console.WriteLine($"[OK] Lieu '{lieu.Nom}' ajouté au biome '{biome.Nom}' !");
            }
            else
            {
                Console.WriteLine("[!] Biome introuvable.");
            }
        }

        public void SupprimerLieu(int id)
        {
            var lieu = _context.LieuxDbSet.Find(id);
            if (lieu != null)
            {
                _context.LieuxDbSet.Remove(lieu);
                _context.SaveChanges();
                Console.WriteLine($"[OK] Lieu '{lieu.Nom}' supprimé !");
            }
            else
            {
                Console.WriteLine("[!] Lieu introuvable.");
            }
        }

        public void ModifierLieu(Lieu lieu)
        {
            var lieuExistant = _context.LieuxDbSet.Find(lieu.Id);
            if (lieuExistant != null)
            {
                lieuExistant.Nom = lieu.Nom;
                lieuExistant.Description = lieu.Description;
                _context.SaveChanges();
                Console.WriteLine($"[OK] Lieu '{lieu.Nom}' modifié !");
            }
            else
            {
                Console.WriteLine("[!] Lieu introuvable.");
            }
        }

        public List<Lieu> ListerLieux()
        {
            return _context.LieuxDbSet
                .Include(l => l.Biome)
                .Include(l => l.Auberge)
                .Include(l => l.Magasin)
                .Include(l => l.Mairie)
                .Include(l => l.Donjon)
                .ToList();
        }

        public List<Lieu> ListerLieuxParBiome(int biomeId)
        {
            return _context.LieuxDbSet
                .Where(l => l.BiomeId == biomeId)
                .Include(l => l.Auberge)
                .Include(l => l.Magasin)
                .Include(l => l.Mairie)
                .Include(l => l.Donjon)
                .ToList();
        }

        public Lieu? ObtenirLieu(int id)
        {
            return _context.LieuxDbSet
                .Include(l => l.Biome)
                .Include(l => l.Auberge)
                .Include(l => l.Magasin)
                .Include(l => l.Mairie)
                .Include(l => l.Donjon)
                .FirstOrDefault(l => l.Id == id);
        }

        // Méthodes utilitaires
        public void AjouterBatimentALieu(int lieuId, Batiment batiment)
        {
            var lieu = _context.LieuxDbSet.Find(lieuId);
            if (lieu == null)
            {
                Console.WriteLine("[!] Lieu introuvable.");
                return;
            }

            switch (batiment)
            {
                case Auberge auberge:
                    lieu.Auberge = auberge;
                    _context.SaveChanges();
                    Console.WriteLine($"[OK] Auberge '{auberge.Nom}' ajoutée au lieu '{lieu.Nom}' !");
                    break;
                case Magasin magasin:
                    lieu.Magasin = magasin;
                    _context.SaveChanges();
                    Console.WriteLine($"[OK] Magasin '{magasin.Nom}' ajouté au lieu '{lieu.Nom}' !");
                    break;
                case Forge forge:
                    lieu.Forge = forge;
                    _context.SaveChanges();
                    Console.WriteLine($"[OK] Forge '{forge.Nom}' ajoutée au lieu '{lieu.Nom}' !");
                    break;
                case Mairie mairie:
                    lieu.Mairie = mairie;
                    _context.SaveChanges();
                    Console.WriteLine($"[OK] Mairie '{mairie.Nom}' ajoutée au lieu '{lieu.Nom}' !");
                    break;
                case Donjon donjon:
                    lieu.Donjon = donjon;
                    _context.SaveChanges();
                    Console.WriteLine($"[OK] Donjon '{donjon.Nom}' ajouté au lieu '{lieu.Nom}' !");
                    break;
                default:
                    Console.WriteLine("[!] Type de bâtiment non reconnu.");
                    break;
            }
        }

        public void SupprimerBatimentDeLieu(int lieuId, int batimentId)
        {
            var lieu = _context.LieuxDbSet
                .Include(l => l.Auberge)
                .Include(l => l.Magasin)
                .Include(l => l.Forge)
                .Include(l => l.Mairie)
                .Include(l => l.Donjon)
                .FirstOrDefault(l => l.Id == lieuId);

            if (lieu == null)
            {
                Console.WriteLine("[!] Lieu introuvable.");
                return;
            }

            bool supprime = false;

            if (lieu.Auberge?.Id == batimentId)
            {
                lieu.Auberge = null;
                supprime = true;
            }
            else if (lieu.Magasin?.Id == batimentId)
            {
                lieu.Magasin = null;
                supprime = true;
            }
            else if (lieu.Forge?.Id == batimentId)
            {
                lieu.Forge = null;
                supprime = true;
            }
            else if (lieu.Mairie?.Id == batimentId)
            {
                lieu.Mairie = null;
                supprime = true;
            }
            else if (lieu.Donjon?.Id == batimentId)
            {
                lieu.Donjon = null;
                supprime = true;
            }

            if (supprime)
            {
                _context.SaveChanges();
                Console.WriteLine($"[OK] Bâtiment supprimé du lieu '{lieu.Nom}' !");
            }
            else
            {
                Console.WriteLine("[!] Bâtiment introuvable dans ce lieu.");
            }
        }
    }
}
