using DataEntity;

namespace RPGEditor.Services
{
    public class ConsommableService
    {
        private readonly RPGContext _context;

        public ConsommableService(RPGContext context)
        {
            _context = context;
        }

        public void CreateConsommable()
        {
            Console.Clear();
            Console.WriteLine("=== CREER UN CONSOMMABLE ===\n");
            
            Console.Write("Nom : ");
            var nom = Console.ReadLine() ?? "";
            
            Console.Write("Valeur (gold) : ");
            var valeur = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Intelligence requise : ");
            var intelligence = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Nombre d'utilisations : ");
            var nbUtilisations = int.Parse(Console.ReadLine() ?? "1");
            
            Console.Write("Effet (0=Soin, 1=Degats, 2=BuffForce, 3=BuffEnergie) : ");
            var effet = (EffetType)int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Valeur de l'effet : ");
            var effetValeur = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Rareté (0=Commun, 1=PeuCommun, 2=Rare, 3=Epique, 4=Legendaire) : ");
            var rarete = (ERarete)int.Parse(Console.ReadLine() ?? "0");

            var consommable = new Consommable(nom, valeur, intelligence, nbUtilisations, effet, effetValeur, rarete);

            _context.ConsommablesDbSet.Add(consommable);
            _context.SaveChanges();
            Console.WriteLine("\n[OK] Consommable créé !");
        }

        public void ListConsommables()
        {
            Console.Clear();
            Console.WriteLine("=== LISTE DES CONSOMMABLES ===\n");
            
            var consommables = _context.ConsommablesDbSet.ToList();
            if (consommables.Count == 0)
            {
                Console.WriteLine("Aucun consommable trouvé.");
                return;
            }

            foreach (var conso in consommables)
            {
                Console.WriteLine($"ID: {conso.Id} | {conso.Nom} | Effet: {conso.Effet} (+{conso.EffetValeur}) | Valeur: {conso.Valeur}g | Utilisations: {conso.NombreUtilisation}");
            }
        }

        public void UpdateConsommable()
        {
            ListConsommables();
            Console.Write("\nID du consommable à modifier : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            
            var consommable = _context.ConsommablesDbSet.Find(id);
            if (consommable == null)
            {
                Console.WriteLine("Consommable introuvable.");
                return;
            }

            Console.WriteLine($"\n=== MODIFIER : {consommable.Nom} ===");
            
            Console.Write($"Nom ({consommable.Nom}) : ");
            var nom = Console.ReadLine();
            if (!string.IsNullOrEmpty(nom)) consommable.Nom = nom;
            
            Console.Write($"Valeur ({consommable.Valeur}) : ");
            if (int.TryParse(Console.ReadLine(), out int valeur)) consommable.Valeur = valeur;
            
            Console.Write($"Valeur effet ({consommable.EffetValeur}) : ");
            if (int.TryParse(Console.ReadLine(), out int effetValeur)) consommable.EffetValeur = effetValeur;

            _context.SaveChanges();
            Console.WriteLine("\n[OK] Consommable modifié !");
        }

        public void DeleteConsommable()
        {
            ListConsommables();
            Console.Write("\nID du consommable à supprimer : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            
            var consommable = _context.ConsommablesDbSet.Find(id);
            if (consommable != null)
            {
                _context.ConsommablesDbSet.Remove(consommable);
                _context.SaveChanges();
                Console.WriteLine("\n[OK] Consommable supprimé !");
            }
            else
            {
                Console.WriteLine("Consommable introuvable.");
            }
        }
    }
}
