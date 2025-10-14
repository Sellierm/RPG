using DataEntity;

namespace RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Consommable1 = new Consommable("Potion de soin", 50, 0, 1, EffetType.Soin,10, ERarete.Rare);
            
            var Forge1 = new Forge("Auberge", "Un endroit pour se reposer et récupérer de l'énergie.");
            var lieux = new List<Lieu>
            {
                new Lieu("Forêt Enchantée", "Une forêt mystérieuse remplie de créatures magiques.",null,Forge1,null,null,null),
            };
            var carte1 = new Carte("Carte de la Forêt", "Une carte détaillée de la Forêt Enchantée.", lieux);
            // Exemple d'instanciation d'un héros avec des valeurs fictives
            var Hero1 = new Hero(
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
            using (RPGContext context = new RPGContext())
            {
                context.ConsommablesDbSet.Add(Consommable1);
                context.CartesDbSet.Add(carte1);
                context.HerosDbSet.Add(Hero1);
                context.SaveChanges();
            }

            //afficher les consommables
            using (RPGContext context = new RPGContext())
            {
                var consommables = context.ConsommablesDbSet.ToList();
                foreach (var consommable in consommables)
                {
                    Console.WriteLine($"Nom: {consommable.Nom}, Valeur: {consommable.Valeur}, Intelligence Requise: {consommable.IntelligenceRequise}, Nombre d'Utilisation: {consommable.NombreUtilisation}, Effet: {consommable.Effet}\nValeur de l'effet: {consommable.EffetValeur} Rareté: {consommable.Rarete}");
                }
                Console.WriteLine("\n");


                var heros = context.HerosDbSet.ToList();
                foreach (var hero in heros)
                {
                    Console.WriteLine($"Nom: {hero.Nom}, Description: {hero.Description}, Points de Vie: {hero.PointsDeVie}, Force: {hero.Force}, Energie: {hero.Energie}, Vitesse: {hero.Vitesse}, Niveau: {hero.Niveau}, Experience: {hero.Experience}, Gold: {hero.Gold}");
                }
                Console.WriteLine("\n");


                var cartes = context.CartesDbSet.ToList();
                foreach (var carte in cartes)
                {
                    Console.WriteLine($"Nom: {carte.Nom}, Description: {carte.Description}");
                    foreach (var lieu in carte.Lieux)
                    {
                        Console.WriteLine($"\tLieu: {lieu.Nom}, Description: {lieu.Description}");
                        if (lieu.Forge != null)
                        {
                            Console.WriteLine($"\t\tForge: {lieu.Forge.Nom}, Description: {lieu.Forge.Description}");
                        }
                        if (lieu.Auberge != null)
                        {
                            Console.WriteLine($"\t\tAuberge: {lieu.Auberge.Nom}, Description: {lieu.Auberge.Description}");
                        }
                        if (lieu.Magasin != null)
                        {
                            Console.WriteLine($"\t\tMagasin: {lieu.Magasin.Nom}, Description: {lieu.Magasin.Description}");
                        }
                        if (lieu.Mairie != null)
                        {
                            Console.WriteLine($"\t\tMairie: {lieu.Mairie.Nom}, Description: {lieu.Mairie.Description}");
                        }
                    }
                }

            }
        }
    }
}
