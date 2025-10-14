using System;
using System.Linq;
using System.Collections.Generic;
using DataEntity;

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
                        break;
                    case "3":
                        Quitter();
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
            Console.WriteLine("2. Explorer un lieu");
            Console.WriteLine("3. Quitter");
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

            // Héros
            if (!_context.HerosDbSet.Any())
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
                _context.HerosDbSet.Add(hero);
                _context.SaveChanges();
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
            var hero = _context.HerosDbSet.FirstOrDefault();
            if (hero is null)
            {
                Console.WriteLine("Aucun héros trouvé.");
                return;
            }

            Console.WriteLine(
                $"Nom: {hero.Nom}, Description: {hero.Description}, " +
                $"PV: {hero.PointsDeVie}, Force: {hero.Force}, Energie: {hero.Energie}, " +
                $"Vitesse: {hero.Vitesse}, Niveau: {hero.Niveau}, XP: {hero.Experience}, Gold: {hero.Gold}"
            );
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