using DataEntity;
using Newtonsoft.Json;

namespace RPG
{
    public class Jeu : IDisposable
    {
        private readonly RPGContext _context;
        private bool _enCours;
        public Hero? HeroActuel { get; private set; }

        public Jeu()
        {
            _context = new RPGContext();
        }

        public void Demarrer()
        {
            InitialiserDonneesSiNecessaire();

            _enCours = true;
            Console.WriteLine("Bienvenue dans le RPG !");
            while (_enCours)
            {
                AfficherMenu();
                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AfficherHero();
                        break;
                    case "2":
                        //ExplorerLieux();
                        Sauvegarder();
                        break;
                    case "3":
                        Quitter();
                        break;
                    case "4":
                        AfficherTousLesHeros();
                        break;
                    default:
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
        }

        private void AfficherMenu()
        {
            Console.WriteLine("\nMenu principal :");
            Console.WriteLine("1. Afficher le héros");
            Console.WriteLine("2. Sauvegarder");
            Console.WriteLine("3. Quitter");
            Console.WriteLine("4. Afficher tous les héros");
            Console.Write("Choix : ");
        }

        private void InitialiserDonneesSiNecessaire()
        {
            // Cartes + Lieux
            if (!_context.CartesDbSet.Any())
            {
                var lieux = new List<Lieu>
                {
                    new Lieu("Forêt Enchantée", "Une forêt mystérieuse remplie de créatures magiques."),
                    new Lieu("Plaines Venteuses", "De vastes étendues d'herbe battues par les vents.")
                };
                var carte = new Carte("Carte du Royaume", "Une carte générale du royaume.", lieux);
                _context.CartesDbSet.Add(carte);
                _context.SaveChanges();
            }

            var files = Directory.GetFiles("../../../../Database/", "*.json");
            // Héros
            if (!files.Any())
            {
                var hero = new Hero(
                    nom: "Arthas",
                    description: "Un chevalier courageux",
                    pointsDeVie: 100,
                    force: 20,
                    energie: 30,
                    vitesse: 15,
                    niveau: 1,
                    experience: 0,
                    gold: 100
                );
                HeroActuel= hero;
                Sauvegarder();
            }

            // Consommables (exemple simple)
            if (!_context.ConsommablesDbSet.Any())
            {
                var potion = new Consommable("Potion de soin", 50, 0, 1, EffetType.Soin, 10, ERarete.Rare);
                _context.ConsommablesDbSet.Add(potion);
                _context.SaveChanges();
            }
        }

        private void AfficherHero()
        {
            var hero = ChargerHero("Arthas");
            /*if (hero is null)
                hero = _context.HerosDbSet.FirstOrDefault();*/
            if (hero is null)
            {
                Console.WriteLine("Aucun héros trouvé, assignation à un héro de base");
                hero = _context.HerosDbSet.FirstOrDefault();
                return;
            }
            HeroActuel = hero;

            Console.WriteLine(
                $"Nom: {hero.Nom}, Description: {hero.Description}, " +
                $"PV: {hero.PointsDeVie}, Force: {hero.Force}, Energie: {hero.Energie}, " +
                $"Vitesse: {hero.Vitesse}, Niveau: {hero.Niveau}, XP: {hero.Experience}, Gold: {hero.Gold}"
            );
        }
        private void AfficherTousLesHeros()
        {
            var heros = ChargerTousLesHeros();
            if (heros.Count == 0)
            {
                Console.WriteLine("Aucun héros trouvé.");
                return;
            }
            Console.WriteLine("Héros disponibles :");
            foreach (var hero in heros)
            {
                Console.WriteLine(
                    $"Nom: {hero.Nom}, Description: {hero.Description}, " +
                    $"PV: {hero.PointsDeVie}, Force: {hero.Force}, Energie: {hero.Energie}, " +
                    $"Vitesse: {hero.Vitesse}, Niveau: {hero.Niveau}, XP: {hero.Experience}, Gold: {hero.Gold}"
                );
            }
        }

        /* private void ExplorerLieux()
         {
             var lieux = _context.CartesDbSet.SelectMany(c => c.Lieux).ToList();
             if (lieux.Count == 0)
             {
                 Console.WriteLine("Aucun lieu disponible.");
                 return;
             }

             Console.WriteLine("Lieux disponibles :");
             for (int i = 0; i < lieux.Count; i++)
             {
                 Console.WriteLine($"{i + 1}. {lieux[i].Nom} - {lieux[i].Description}");
             }
             Console.Write("Choisissez un lieu (numéro) ou Entrée pour annuler: ");
             var choix = Console.ReadLine();
             if (int.TryParse(choix, out int index) && index >= 1 && index <= lieux.Count)
             {
                 var lieu = lieux[index - 1];
                 Console.WriteLine($"Vous explorez: {lieu.Nom}. {lieu.Description}");
                 // Ici, vous pouvez déclencher événements/rencontres/combat.
             }
         }*/


        public void EntrerDans(Batiment batiment)
        {
            HeroActuel.BatimentActuel = batiment;
            Console.WriteLine($"{HeroActuel.Nom} entre dans {batiment.Nom}.");
        }
        public void AfficherActionsDisponibles()
        {
            if (HeroActuel.BatimentActuel == null)
            {
                Console.WriteLine($"{HeroActuel.Nom} est à l’extérieur. Aucune action disponible.");
                return;
            }

            Console.WriteLine($"Actions disponibles dans {HeroActuel.BatimentActuel.Nom} :");

            switch (HeroActuel.BatimentActuel)
            {
                case Auberge:
                    Console.WriteLine(" - Dormir");
                    break;

                case Forge:
                    Console.WriteLine(" - Forger");
                    break;

                case Mairie:
                    Console.WriteLine(" - Parler au maire");
                    break;

                default:
                    Console.WriteLine("Aucune action spécifique.");
                    break;
            }
        }
        public void ExecuterAction(string action)
        {
            if (HeroActuel.BatimentActuel == null)
            {
                Console.WriteLine("Le héros n'est dans aucun bâtiment.");
                return;
            }

            switch (HeroActuel.BatimentActuel)
            {
                case Auberge when action.Equals("Dormir", StringComparison.OrdinalIgnoreCase):
                    Dormir();
                    break;

                case Forge when action.Equals("Forger", StringComparison.OrdinalIgnoreCase):
                    Forger();
                    break;

                case Mairie when action.Equals("Sauvegarder", StringComparison.OrdinalIgnoreCase):
                    Sauvegarder();
                    break;

                default:
                    Console.WriteLine("Action inconnue ou non disponible ici.");
                    break;
            }
        }

        // --- Actions spécifiques ---
        private void Dormir()
        {
            HeroActuel.Energie = Math.Min(100, HeroActuel.Energie + 50);
            Console.WriteLine($"{HeroActuel.Nom} dort à l'auberge et récupère de l'énergie ({HeroActuel.Energie}/100).");
        }
        private void Forger()
        {
            HeroActuel.Energie -= 20;
            Console.WriteLine($"{HeroActuel.Nom} forge une arme. Fatigue : {HeroActuel.Energie}/100.");
        }
        // ---                     ---

        // ---    Fichiers json    ---
        private void Sauvegarder()
        {
            // Logic to save the hero state into json file with newtonsoft.json
            Console.WriteLine($"{HeroActuel.Nom} sauvegarde la partie.");
            var json = JsonConvert.SerializeObject(HeroActuel, Formatting.Indented,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto, // Important si tu veux garder les types dérivés
                NullValueHandling = NullValueHandling.Ignore // Ne pas écrire les valeurs null
            });

            File.WriteAllText($"../../../../Database/{HeroActuel.Nom}.json", json);
            Console.WriteLine("Héros sauvegardé");

        }
        public static Hero? ChargerHero(string nomHero)
        {
            if (!File.Exists($"../../../../Database/{nomHero}.json"))
            {
                Console.WriteLine("Fichier de sauvegarde introuvable.");
                return null;
            }

            var json = File.ReadAllText($"../../../../Database/{nomHero}.json");
            return JsonConvert.DeserializeObject<Hero>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
        }
        public static List<Hero> ChargerTousLesHeros()
        {
            var heros = new List<Hero>();
            var files = Directory.GetFiles("../../../../Database/", "*.json");
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var hero = JsonConvert.DeserializeObject<Hero>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                if (hero != null)
                    heros.Add(hero);
            }
            return heros;
        }
        // ---                    ---
        private void Quitter()
        {
            Console.WriteLine("Merci d'avoir joué !");
            _enCours = false;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}