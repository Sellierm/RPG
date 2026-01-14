using DataEntity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataEntity.Services
{
    public class ObjetService : IObjetService
    {
        private readonly RPGContext _context;

        public ObjetService(RPGContext context)
        {
            _context = context;
        }

        // ===== GESTION DES ARMES =====
        
        // Ajoute une nouvelle arme dans la base de données
        // L'arme sera disponible dans tous les magasins
        public void AjouterArme(Arme arme)
        {
            _context.Set<Arme>().Add(arme);
            _context.SaveChanges();
            Console.WriteLine($"[OK] Arme '{arme.Nom}' ajoutée.");
        }

        // Supprime une arme existante de la base
        public void SupprimerArme(int id)
        {
            var arme = _context.Set<Arme>().Find(id);
            if (arme != null)
            {
                _context.Set<Arme>().Remove(arme);
                _context.SaveChanges();
                Console.WriteLine($"[OK] Arme supprimée.");
            }
            else
            {
                Console.WriteLine("Arme introuvable.");
            }
        }

        // Modifie les caractéristiques d'une arme existante
        // Permet d'ajuster dégâts, prix, effets, etc.
        public void ModifierArme(Arme arme)
        {
            _context.Set<Arme>().Update(arme);
            _context.SaveChanges();
            Console.WriteLine($"[OK] Arme '{arme.Nom}' modifiée.");
        }

        // Récupère la liste complète des armes du jeu
        public List<Arme> ListerArmes()
        {
            return _context.Set<Arme>().ToList();
        }

        // Récupère une arme spécifique par son identifiant
        public Arme? ObtenirArme(int id)
        {
            return _context.Set<Arme>().Find(id);
        }

        // ===== GESTION DES ARMURES =====
        
        // Ajoute une nouvelle armure dans la base de données
        // L'armure sera disponible dans tous les magasins
        public void AjouterArmure(Armure armure)
        {
            _context.Set<Armure>().Add(armure);
            _context.SaveChanges();
            Console.WriteLine($"[OK] Armure '{armure.Nom}' ajoutée.");
        }

        // Supprime une armure existante de la base
        public void SupprimerArmure(int id)
        {
            var armure = _context.Set<Armure>().Find(id);
            if (armure != null)
            {
                _context.Set<Armure>().Remove(armure);
                _context.SaveChanges();
                Console.WriteLine($"[OK] Armure supprimée.");
            }
            else
            {
                Console.WriteLine("Armure introuvable.");
            }
        }

        // Modifie les caractéristiques d'une armure existante
        // Permet d'ajuster défense, prix, effets, etc.
        public void ModifierArmure(Armure armure)
        {
            _context.Set<Armure>().Update(armure);
            _context.SaveChanges();
            Console.WriteLine($"[OK] Armure '{armure.Nom}' modifiée.");
        }

        // Récupère la liste complète des armures du jeu
        public List<Armure> ListerArmures()
        {
            return _context.Set<Armure>().ToList();
        }

        // Récupère une armure spécifique par son identifiant
        public Armure? ObtenirArmure(int id)
        {
            return _context.Set<Armure>().Find(id);
        }

        // ===== GESTION DES CONSOMMABLES =====
        
        // Ajoute un nouveau consommable (potion, élixir, etc.)
        // Le consommable sera disponible dans tous les magasins
        public void AjouterConsommable(Consommable consommable)
        {
            _context.ConsommablesDbSet.Add(consommable);
            _context.SaveChanges();
            Console.WriteLine($"[OK] Consommable '{consommable.Nom}' ajouté.");
        }

        // Supprime un consommable existant de la base
        public void SupprimerConsommable(int id)
        {
            var consommable = _context.ConsommablesDbSet.Find(id);
            if (consommable != null)
            {
                _context.ConsommablesDbSet.Remove(consommable);
                _context.SaveChanges();
                Console.WriteLine($"[OK] Consommable supprimé.");
            }
            else
            {
                Console.WriteLine("Consommable introuvable.");
            }
        }

        // Modifie les caractéristiques d'un consommable existant
        // Permet d'ajuster effets, nombre d'utilisations, prix, etc.
        public void ModifierConsommable(Consommable consommable)
        {
            _context.ConsommablesDbSet.Update(consommable);
            _context.SaveChanges();
            Console.WriteLine($"[OK] Consommable '{consommable.Nom}' modifié.");
        }

        // Récupère la liste complète des consommables du jeu
        public List<Consommable> ListerConsommables()
        {
            return _context.ConsommablesDbSet.ToList();
        }

        // Récupère un consommable spécifique par son identifiant
        public Consommable? ObtenirConsommable(int id)
        {
            return _context.ConsommablesDbSet.Find(id);
        }
    }
}
