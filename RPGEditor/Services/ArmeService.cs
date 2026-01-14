using DataEntity;

namespace RPGEditor.Services
{
    public class ArmeService
    {
        private readonly RPGContext _context;

        public ArmeService(RPGContext context)
        {
            _context = context;
        }

        // Crée une nouvelle arme via une interface console interactive
        // Demande toutes les caractéristiques et l'ajoute à la base
        public void CreateArme()
        {
            Console.Clear();
            Console.WriteLine("=== CREER UNE ARME ===\n");
            
            Console.Write("Nom : ");
            var nom = Console.ReadLine() ?? "";
            
            Console.Write("Valeur (gold) : ");
            var valeur = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Intelligence requise : ");
            var intelligence = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Dégâts : ");
            var degats = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Type (Épée/Hache/Dague/Arc) : ");
            var type = Console.ReadLine() ?? "Épée";
            
            Console.Write("Rareté (0=Commun, 1=PeuCommun, 2=Rare, 3=Epique, 4=Legendaire) : ");
            var rarete = (ERarete)int.Parse(Console.ReadLine() ?? "0");

            var arme = new Arme
            {
                Nom = nom,
                Valeur = valeur,
                IntelligenceRequise = intelligence,
                Degats = degats,
                Type = type,
                Effet = EffetType.Aucun,
                EffetValeur = 0,
                Rarete = rarete
            };

            _context.Set<Arme>().Add(arme);
            _context.SaveChanges();
            Console.WriteLine("\n[OK] Arme créée !");
        }

        // Affiche la liste complète de toutes les armes en base
        // Montre l'ID, nom, dégâts, prix et rareté
        public void ListArmes()
        {
            Console.Clear();
            Console.WriteLine("=== LISTE DES ARMES ===\n");
            
            var armes = _context.Set<Arme>().ToList();
            if (armes.Count == 0)
            {
                Console.WriteLine("Aucune arme trouvée.");
                return;
            }

            foreach (var arme in armes)
            {
                Console.WriteLine($"ID: {arme.Id} | {arme.Nom} | Dégâts: {arme.Degats} | Valeur: {arme.Valeur}g | Rareté: {arme.Rarete}");
            }
        }

        // Modifie une arme existante via interface interactive
        // Permet de changer n'importe quelle caractéristique
        public void UpdateArme()
        {
            ListArmes();
            Console.Write("\nID de l'arme à modifier : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            
            var arme = _context.Set<Arme>().Find(id);
            if (arme == null)
            {
                Console.WriteLine("Arme introuvable.");
                return;
            }

            Console.WriteLine($"\n=== MODIFIER : {arme.Nom} ===");
            
            Console.Write($"Nom ({arme.Nom}) : ");
            var nom = Console.ReadLine();
            if (!string.IsNullOrEmpty(nom)) arme.Nom = nom;
            
            Console.Write($"Valeur ({arme.Valeur}) : ");
            if (int.TryParse(Console.ReadLine(), out int valeur)) arme.Valeur = valeur;
            
            Console.Write($"Dégâts ({arme.Degats}) : ");
            if (int.TryParse(Console.ReadLine(), out int degats)) arme.Degats = degats;

            _context.SaveChanges();
            Console.WriteLine("\n[OK] Arme modifiée !");
        }

        // Supprime définitivement une arme de la base
        // Demande l'ID de l'arme à supprimer
        public void DeleteArme()
        {
            ListArmes();
            Console.Write("\nID de l'arme à supprimer : ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            
            var arme = _context.Set<Arme>().Find(id);
            if (arme != null)
            {
                _context.Set<Arme>().Remove(arme);
                _context.SaveChanges();
                Console.WriteLine("\n[OK] Arme supprimée !");
            }
            else
            {
                Console.WriteLine("Arme introuvable.");
            }
        }
    }
}
