using DataEntity;


namespace RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Consommable1 = new Consommable("Potion de soin", 50, 0, 1, EffetType.Soin,10, ERarete.Rare);
            using (RPGContext context = new RPGContext())
            {
                context.ConsommablesDbSet.Add(Consommable1);
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
            }

            // Même principe pour les ennemis, cartes, etc.
        }
    }
}
