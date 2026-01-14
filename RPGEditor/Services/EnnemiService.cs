using DataEntity;

namespace RPGEditor.Services
{
    public class EnnemiService
    {
        private readonly RPGContext _context;

        public EnnemiService(RPGContext context)
        {
            _context = context;
        }

        public void CreateEnnemi()
        {
            Console.Clear();
            Console.WriteLine("=== CREER UN ENNEMI ===\n");
            
            Console.Write("Nom : ");
            var nom = Console.ReadLine() ?? "";
            
            Console.Write("Description : ");
            var description = Console.ReadLine() ?? "";
            
            Console.Write("Points de vie : ");
            var pv = int.Parse(Console.ReadLine() ?? "50");
            
            Console.Write("Force : ");
            var force = int.Parse(Console.ReadLine() ?? "10");
            
            Console.Write("Énergie : ");
            var energie = int.Parse(Console.ReadLine() ?? "30");
            
            Console.Write("Vitesse : ");
            var vitesse = int.Parse(Console.ReadLine() ?? "10");
            
            Console.Write("Race (0=Humain, 1=Elfe, 2=Nain, 3=Orc, 4=Gobelin, 5=Bete, 6=MortVivant, 7=Demon) : ");
            var race = (ERace)int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("XP donnée : ");
            var xp = int.Parse(Console.ReadLine() ?? "25");
            
            Console.Write("Gold donné : ");
            var gold = int.Parse(Console.ReadLine() ?? "15");

            var ennemi = new Ennemi
            {
                Nom = nom,
                Description = description,
                PointsDeVie = pv,
                Force = force,
                Energie = energie,
                Vitesse = vitesse,
                Race = race,
                XpValeur = xp,
                GoldValeur = gold
            };

            _context.Set<Ennemi>().Add(ennemi);
            _context.SaveChanges();
            Console.WriteLine("\n[OK] Ennemi créé !");
        }

        public void ListEnnemis()
        {
            Console.Clear();
            Console.WriteLine("=== LISTE DES ENNEMIS ===\n");
            
            var ennemis = _context.Set<Ennemi>().ToList();
            if (ennemis.Count == 0)
            {
                Console.WriteLine("Aucun ennemi trouvé.");
                return;
            }

            foreach (var ennemi in ennemis)
            {
                Console.WriteLine($"ID: {ennemi.Id} | {ennemi.Nom} ({ennemi.Race}) | PV: {ennemi.PointsDeVie} | Force: {ennemi.Force} | XP: {ennemi.XpValeur} | Gold: {ennemi.GoldValeur}");
            }
        }

        public void UpdateEnnemi()
        {
            ListEnnemis();
            Console.Write("\nID de l'ennemi à modifier : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            
            var ennemi = _context.Set<Ennemi>().Find(id);
            if (ennemi == null)
            {
                Console.WriteLine("Ennemi introuvable.");
                return;
            }

            Console.WriteLine($"\n=== MODIFIER : {ennemi.Nom} ===");
            
            Console.Write($"Nom ({ennemi.Nom}) : ");
            var nom = Console.ReadLine();
            if (!string.IsNullOrEmpty(nom)) ennemi.Nom = nom;
            
            Console.Write($"Points de vie ({ennemi.PointsDeVie}) : ");
            if (int.TryParse(Console.ReadLine(), out int pv)) ennemi.PointsDeVie = pv;
            
            Console.Write($"Force ({ennemi.Force}) : ");
            if (int.TryParse(Console.ReadLine(), out int force)) ennemi.Force = force;
            
            Console.Write($"XP donnée ({ennemi.XpValeur}) : ");
            if (int.TryParse(Console.ReadLine(), out int xp)) ennemi.XpValeur = xp;
            
            Console.Write($"Gold donné ({ennemi.GoldValeur}) : ");
            if (int.TryParse(Console.ReadLine(), out int gold)) ennemi.GoldValeur = gold;

            _context.SaveChanges();
            Console.WriteLine("\n[OK] Ennemi modifié !");
        }

        public void DeleteEnnemi()
        {
            ListEnnemis();
            Console.Write("\nID de l'ennemi à supprimer : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            
            var ennemi = _context.Set<Ennemi>().Find(id);
            if (ennemi != null)
            {
                _context.Set<Ennemi>().Remove(ennemi);
                _context.SaveChanges();
                Console.WriteLine("\n[OK] Ennemi supprimé !");
            }
            else
            {
                Console.WriteLine("Ennemi introuvable.");
            }
        }
    }
}
