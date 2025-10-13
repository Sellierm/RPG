using DataEntity;
using DataEntity.Enum;
using DataEntity.Objets;
using DataEntity.Personnages;
using DataEntity.Cartes;


namespace RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*var Consommable1 = new Consommable("Potion de soin", 50, 0, 1, EffetType.Soin,10, ERarete.Rare);
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
            );*/

            using (RPGContext context = new RPGContext())
            {
                /*context.ConsommablesDbSet.Add(Consommable1);
                context.HerosDbSet.Add(Hero1);*/
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

                var heros = context.HerosDbSet.ToList();
                foreach (var hero in heros)
                {
                    Console.WriteLine($"Nom: {hero.Nom}, Description: {hero.Description}, Points de Vie: {hero.PointsDeVie}, Force: {hero.Force}, Energie: {hero.Energie}, Vitesse: {hero.Vitesse}, Niveau: {hero.Niveau}, Experience: {hero.Experience}, Gold: {hero.Gold}");
                }
            }

            // Même principe pour les ennemis, cartes, etc.
        }
    }
}
