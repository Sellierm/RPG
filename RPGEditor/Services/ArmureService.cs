using DataEntity;

namespace RPGEditor.Services
{
    public class ArmureService
    {
        private readonly RPGContext _context;

        public ArmureService(RPGContext context)
        {
            _context = context;
        }

        public void CreateArmure()
        {
            Console.Clear();
            Console.WriteLine("=== CREER UNE ARMURE ===\n");
            
            Console.Write("Nom : ");
            var nom = Console.ReadLine() ?? "";
            
            Console.Write("Valeur (gold) : ");
            var valeur = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Intelligence requise : ");
            var intelligence = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Défense : ");
            var defense = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Type (Légère/Lourde/Magique) : ");
            var type = Console.ReadLine() ?? "Légère";
            
            Console.Write("Rareté (0=Commun, 1=PeuCommun, 2=Rare, 3=Epique, 4=Legendaire) : ");
            var rarete = (ERarete)int.Parse(Console.ReadLine() ?? "0");

            var armure = new Armure(nom, valeur, intelligence, defense, type, EffetType.Aucun, 0, rarete);

            _context.Set<Armure>().Add(armure);
            _context.SaveChanges();
            Console.WriteLine("\n[OK] Armure créée !");
        }

        public void ListArmures()
        {
            Console.Clear();
            Console.WriteLine("=== LISTE DES ARMURES ===\n");
            
            var armures = _context.Set<Armure>().ToList();
            if (armures.Count == 0)
            {
                Console.WriteLine("Aucune armure trouvée.");
                return;
            }

            foreach (var armure in armures)
            {
                Console.WriteLine($"ID: {armure.Id} | {armure.Nom} | Défense: {armure.Defense} | Valeur: {armure.Valeur}g | Rareté: {armure.Rarete}");
            }
        }

        public void UpdateArmure()
        {
            ListArmures();
            Console.Write("\nID de l'armure à modifier : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            
            var armure = _context.Set<Armure>().Find(id);
            if (armure == null)
            {
                Console.WriteLine("Armure introuvable.");
                return;
            }

            Console.WriteLine($"\n=== MODIFIER : {armure.Nom} ===");
            
            Console.Write($"Nom ({armure.Nom}) : ");
            var nom = Console.ReadLine();
            if (!string.IsNullOrEmpty(nom)) armure.Nom = nom;
            
            Console.Write($"Valeur ({armure.Valeur}) : ");
            if (int.TryParse(Console.ReadLine(), out int valeur)) armure.Valeur = valeur;
            
            Console.Write($"Défense ({armure.Defense}) : ");
            if (int.TryParse(Console.ReadLine(), out int defense)) armure.Defense = defense;

            _context.SaveChanges();
            Console.WriteLine("\n[OK] Armure modifiée !");
        }

        public void DeleteArmure()
        {
            ListArmures();
            Console.Write("\nID de l'armure à supprimer : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            
            var armure = _context.Set<Armure>().Find(id);
            if (armure != null)
            {
                _context.Set<Armure>().Remove(armure);
                _context.SaveChanges();
                Console.WriteLine("\n[OK] Armure supprimée !");
            }
            else
            {
                Console.WriteLine("Armure introuvable.");
            }
        }
    }
}
