using DataEntity.Interfaces;

namespace DataEntity.Services
{
    public class EnnemiService : IEnnemiService
    {
        private readonly RPGContext _context;
        private readonly Random _random;

        public EnnemiService(RPGContext context)
        {
            _context = context;
            _random = new Random();
        }

        // Ajoute un nouveau template d'ennemi dans la base de données
        // Ce template sera utilisé pour générer des ennemis dans le jeu
        public void AjouterEnnemi(Ennemi ennemi)
        {
            _context.Set<Ennemi>().Add(ennemi);
            _context.SaveChanges();
            Console.WriteLine($"[OK] Ennemi '{ennemi.Nom}' ajouté.");
        }

        // Supprime définitivement un ennemi de la base
        public void SupprimerEnnemi(int id)
        {
            var ennemi = _context.Set<Ennemi>().Find(id);
            if (ennemi != null)
            {
                _context.Set<Ennemi>().Remove(ennemi);
                _context.SaveChanges();
                Console.WriteLine($"[OK] Ennemi supprimé.");
            }
            else
            {
                Console.WriteLine("Ennemi introuvable.");
            }
        }

        // Modifie les stats d'un ennemi existant
        // Permet d'ajuster difficulté, récompenses, etc.
        public void ModifierEnnemi(Ennemi ennemi)
        {
            _context.Set<Ennemi>().Update(ennemi);
            _context.SaveChanges();
            Console.WriteLine($"[OK] Ennemi '{ennemi.Nom}' modifié.");
        }

        // Récupère la liste complète des templates d'ennemis
        public List<Ennemi> ListerEnnemis()
        {
            return _context.Set<Ennemi>().ToList();
        }

        // Récupère un ennemi spécifique par son identifiant
        public Ennemi? ObtenirEnnemi(int id)
        {
            return _context.Set<Ennemi>().Find(id);
        }

        // Génère un ennemi adapté à un niveau précis
        // Choisit un template aléatoire et scale ses stats
        public Ennemi GenererEnnemiParNiveau(int niveau)
        {
            var ennemisDisponibles = _context.Set<Ennemi>().ToList();
            
            if (ennemisDisponibles.Any())
            {
                var modele = ennemisDisponibles[_random.Next(ennemisDisponibles.Count)];
                
                return new Ennemi
                {
                    Nom = $"{modele.Nom} (Niv.{niveau})",
                    Description = modele.Description,
                    PointsDeVie = modele.PointsDeVie + (niveau * 10),
                    Force = modele.Force + (niveau * 2),
                    Energie = modele.Energie,
                    Vitesse = modele.Vitesse + niveau,
                    Race = modele.Race,
                    XpValeur = niveau * 25,
                    GoldValeur = niveau * 15
                };
            }
            
            return new Ennemi
            {
                Nom = $"Créature (Niv.{niveau})",
                Description = "Une créature hostile",
                PointsDeVie = 50 + (niveau * 10),
                Force = 10 + (niveau * 3),
                Energie = 50,
                Vitesse = 5 + (niveau * 2),
                Race = ERace.Humain,
                XpValeur = niveau * 25,
                GoldValeur = niveau * 10
            };
        }

        // Génère un ennemi pour un biome spécifique
        // Le niveau est limité par le niveau max du biome pour équilibrer la difficulté
        public Ennemi GenererEnnemiPourBiome(int niveauHero, int niveauMaxBiome)
        {
            // Le niveau de l'ennemi est égal au niveau du héros, mais capé par le niveau max du biome
            int niveauEnnemi = Math.Min(niveauHero, niveauMaxBiome);
            
            var ennemisDisponibles = _context.Set<Ennemi>().ToList();
            
            if (ennemisDisponibles.Any())
            {
                var modele = ennemisDisponibles[_random.Next(ennemisDisponibles.Count)];
                
                return new Ennemi
                {
                    Nom = $"{modele.Nom} (Niv.{niveauEnnemi})",
                    Description = modele.Description,
                    PointsDeVie = modele.PointsDeVie + (niveauEnnemi * 10),
                    Force = modele.Force + (niveauEnnemi * 2),
                    Energie = modele.Energie,
                    Vitesse = modele.Vitesse + niveauEnnemi,
                    Race = modele.Race,
                    XpValeur = niveauEnnemi * 25,
                    GoldValeur = niveauEnnemi * 15
                };
            }
            
            return new Ennemi
            {
                Nom = $"Créature (Niv.{niveauEnnemi})",
                Description = "Une créature hostile",
                PointsDeVie = 50 + (niveauEnnemi * 10),
                Force = 10 + (niveauEnnemi * 3),
                Energie = 50,
                Vitesse = 5 + (niveauEnnemi * 2),
                Race = ERace.Humain,
                XpValeur = niveauEnnemi * 25,
                GoldValeur = niveauEnnemi * 10
            };
        }
    }
}
