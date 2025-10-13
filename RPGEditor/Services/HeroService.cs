using DataEntity;
using DataEntity.Personnages;

namespace RPGEditor.Services
{
    public class HeroService
    {
        private RPGContext _context;

        public HeroService(RPGContext context)
        {
            _context = context;
        }

        public void CreateHero()
        {
            Console.Write("Nom : ");
            var nom = Console.ReadLine() ?? "";
            Console.Write("Description : ");
            var description = Console.ReadLine() ?? "";
            Console.Write("Points de Vie : ");
            var pointsDeVie = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Force : ");
            var force = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Energie : ");
            var energie = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Vitesse : ");
            var vitesse = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Niveau : ");
            var niveau = int.Parse(Console.ReadLine() ?? "1");
            Console.Write("Experience : ");
            var experience = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Gold : ");
            var gold = int.Parse(Console.ReadLine() ?? "0");

            var hero = new Hero(nom, description, pointsDeVie, force, energie, vitesse, niveau, experience, gold);
            _context.HerosDbSet.Add(hero);
            _context.SaveChanges();
            Console.WriteLine("Héros créé !");
        }
        public void DeleteHero()
        {
            ListHeroes();
            Console.Write("Entrez l'ID du héros à supprimer : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var hero = _context.HerosDbSet.Find(id);
            if (hero != null)
            {
                _context.HerosDbSet.Remove(hero);
                _context.SaveChanges();
                Console.WriteLine("Héros supprimé !");
            }
            else
            {
                Console.WriteLine("Héros non trouvé.");
            }
        }
        public void UpdateHero()
        {
            ListHeroes();
            Console.Write("Entrez l'ID du héros à modifier : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var hero = _context.HerosDbSet.Find(id);
            if (hero != null)
            {
                Console.Write($"Nom ({hero.Nom}) : ");
                var nom = Console.ReadLine();
                if (!string.IsNullOrEmpty(nom)) hero.Nom = nom;
                Console.Write($"Description ({hero.Description}) : ");
                var description = Console.ReadLine();
                if (!string.IsNullOrEmpty(description)) hero.Description = description;
                Console.Write($"Points de Vie ({hero.PointsDeVie}) : ");
                var pointsDeVieInput = Console.ReadLine();
                if (int.TryParse(pointsDeVieInput, out int pointsDeVie)) hero.PointsDeVie = pointsDeVie;
                Console.Write($"Force ({hero.Force}) : ");
                var forceInput = Console.ReadLine();
                if (int.TryParse(forceInput, out int force)) hero.Force = force;
                Console.Write($"Energie ({hero.Energie}) : ");
                var energieInput = Console.ReadLine();
                if (int.TryParse(energieInput, out int energie)) hero.Energie = energie;
                Console.Write($"Vitesse ({hero.Vitesse}) : ");
                var vitesseInput = Console.ReadLine();
                if (int.TryParse(vitesseInput, out int vitesse)) hero.Vitesse = vitesse;
                Console.Write($"Niveau ({hero.Niveau}) : ");
                var niveauInput = Console.ReadLine();
                if (int.TryParse(niveauInput, out int niveau)) hero.Niveau = niveau;
                Console.Write($"Experience ({hero.Experience}) : ");
                var experienceInput = Console.ReadLine();
                if (int.TryParse(experienceInput, out int experience)) hero.Experience = experience;
                Console.Write($"Gold ({hero.Gold}) : ");
                var goldInput = Console.ReadLine();
                if (int.TryParse(goldInput, out int gold)) hero.Gold = gold;
                _context.SaveChanges();
                Console.WriteLine("Héros mis à jour !");
            }
            else
            {
                Console.WriteLine("Héros non trouvé.");
            }
        }
        public void ListHeroes()
        {
            var heros = _context.HerosDbSet.ToList();
            foreach (var hero in heros)
            {
                Console.WriteLine($"Id: {hero.Id}, Nom: {hero.Nom}, Description: {hero.Description}, Points de Vie: {hero.PointsDeVie}, Force: {hero.Force}, Energie: {hero.Energie}, Vitesse: {hero.Vitesse}, Niveau: {hero.Niveau}, Experience: {hero.Experience}, Gold: {hero.Gold}");
            }
        }
    }
}