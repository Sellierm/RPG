using DataEntity;
using RPGEditor.Services;

namespace RPGEditor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new RPGContext();
            var heroService = new HeroService(context);
            while (true)
            {
                Console.WriteLine("Opérations sur les héros :");
                Console.WriteLine("1. Créer");
                Console.WriteLine("2. Lire");
                Console.WriteLine("3. Modifier");
                Console.WriteLine("4. Supprimer");
                Console.WriteLine("5. Quitter");
                int choix = int.Parse(Console.ReadLine() ?? "5");

                switch (choix)
                {
                    case 1: heroService.CreateHero(); break;
                    case 2: heroService.ListHeroes(); break;
                    case 3: heroService.UpdateHero(); break;
                    case 4: heroService.DeleteHero(); break;
                    case 5: return;
                    default: Console.WriteLine("Choix invalide."); break;
                }
            }
        }
    }
}
