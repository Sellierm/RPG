using DataEntity.Interfaces;

namespace DataEntity.API
{
    public class RPGAPI
    {
        private static RPGAPI? _instance = null;
        private static RPGContext? _context = null;
        private static IAubergeService? _aubergeService = null;
        private static IMagasinService? _magasinService = null;
        private static ICombatService? _combatService = null;
        private static ISauvegardeService? _sauvegardeService = null;
        private static IObjetService? _objetService = null;
        private static IEnnemiService? _ennemiService = null;
        private static ICarteService? _carteService = null;

        private RPGAPI() { }

        // Récupère l'instance unique de l'API (pattern Singleton)
        // Crée l'instance et le contexte de base de données si nécessaire
        public static RPGAPI GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RPGAPI();
                _context = new RPGContext();
            }
            return _instance;
        }

        // Récupère le service de gestion des auberges
        // Crée le service la première fois qu'il est demandé
        public IAubergeService GetAubergeService()
        {
            if (_aubergeService == null)
            {
                _aubergeService = new Services.AubergeService();
            }
            return _aubergeService;
        }

        // Récupère le service de gestion des magasins
        // Crée le service la première fois qu'il est demandé
        public IMagasinService GetMagasinService()
        {
            if (_magasinService == null && _context != null)
            {
                _magasinService = new Services.MagasinService(_context);
            }
            return _magasinService!;
        }

        // Récupère le service de gestion des combats
        // Crée le service la première fois qu'il est demandé
        public ICombatService GetCombatService()
        {
            if (_combatService == null && _context != null)
            {
                _combatService = new Services.CombatService(_context);
            }
            return _combatService!;
        }

        // Récupère le service de gestion des sauvegardes
        // Crée le service la première fois qu'il est demandé
        public ISauvegardeService GetSauvegardeService()
        {
            if (_sauvegardeService == null && _context != null)
            {
                _sauvegardeService = new Services.SauvegardeService(_context);
            }
            return _sauvegardeService!;
        }

        // Récupère le service de gestion des objets (armes, armures, consommables)
        // Crée le service la première fois qu'il est demandé
        public IObjetService GetObjetService()
        {
            if (_objetService == null && _context != null)
            {
                _objetService = new Services.ObjetService(_context);
            }
            return _objetService!;
        }

        // Récupère le service de gestion des ennemis
        // Crée le service la première fois qu'il est demandé
        public IEnnemiService GetEnnemiService()
        {
            if (_ennemiService == null && _context != null)
            {
                _ennemiService = new Services.EnnemiService(_context);
            }
            return _ennemiService!;
        }

        // Récupère le service de gestion de la carte du monde
        // Crée le service la première fois qu'il est demandé
        public ICarteService GetCarteService()
        {
            if (_carteService == null && _context != null)
            {
                _carteService = new Services.CarteService(_context);
            }
            return _carteService!;
        }

        // Récupère directement le contexte de base de données
        // Utilisé par les services qui ont besoin d'accéder à la DB
        public RPGContext GetContext()
        {
            return _context!;
        }

        // Libère les ressources du contexte de base de données
        // À appeler à la fin du jeu pour fermer proprement la connexion
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
