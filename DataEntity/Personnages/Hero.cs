namespace DataEntity
{
    public class Hero : Personnage
    {
        public int Niveau { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }
        public virtual Batiment? BatimentActuel { get; set; }
        public virtual List<Consommable> InventaireConsommable { get; set; } = new List<Consommable>();
        public virtual List<Arme> InventaireArme { get; set; } = new List<Arme>();
        public virtual Arme? ArmeEquipee { get; set; }
        public virtual List<Armure> InventaireArmure { get; set; } = new List<Armure>();
        public virtual Armure? ArmureEquipee { get; set; }

        //constructor,
        public Hero() { }
        public Hero(string nom, string description, int pointsDeVie, int force, int energie, int vitesse, int niveau, int experience, int gold)
        {
            Nom = nom;
            Description = description;
            PointsDeVie = pointsDeVie;
            Force = force;
            Energie = energie;
            Vitesse = vitesse;
            Niveau = niveau;
            Experience = experience;
            Gold = gold;

            BatimentActuel = null;
            InventaireConsommable = new List<Consommable>();
            InventaireArme = new List<Arme>();

        }

        public void EntrerDans(Batiment batiment)
        {
            BatimentActuel = batiment;
            Console.WriteLine($"{Nom} entre dans {batiment.Nom}.");
        }

        public void AfficherActionsDisponibles()
        {
            if (BatimentActuel == null)
            {
                Console.WriteLine($"{Nom} est à l’extérieur. Aucune action disponible.");
                return;
            }

            Console.WriteLine($"Actions disponibles dans {BatimentActuel.Nom} :");

            switch (BatimentActuel)
            {
                case Auberge:
                    Console.WriteLine(" - Dormir");
                    break;

                case Forge:
                    Console.WriteLine(" - Forger");
                    break;

                case Mairie:
                    Console.WriteLine(" - Parler au maire");
                    break;

                default:
                    Console.WriteLine("Aucune action spécifique.");
                    break;
            }
        }

        public void ExecuterAction(string action)
        {
            if (BatimentActuel == null)
            {
                Console.WriteLine("Le héros n'est dans aucun bâtiment.");
                return;
            }

            switch (BatimentActuel)
            {
                case Auberge when action.Equals("Dormir", StringComparison.OrdinalIgnoreCase):
                    Dormir();
                    break;

                case Forge when action.Equals("Forger", StringComparison.OrdinalIgnoreCase):
                    Forger();
                    break;

                case Mairie when action.Equals("Sauvegarder", StringComparison.OrdinalIgnoreCase):
                    Sauvegarder();
                    break;

                default:
                    Console.WriteLine("Action inconnue ou non disponible ici.");
                    break;
            }
        }

        // --- Actions spécifiques ---
        private void Dormir()
        {
            Energie = Math.Min(100, Energie + 50);
            Console.WriteLine($"{Nom} dort à l'auberge et récupère de l'énergie ({Energie}/100).");
        }

        private void Forger()
        {
            Energie -= 20;
            Console.WriteLine($"{Nom} forge une arme. Fatigue : {Energie}/100.");
        }

        private void Sauvegarder()
        {
            // Logic to save the hero state into json file with newtonsoft.json
            Console.WriteLine($"{Nom} sauvegarde la partie.");
        }
    }
}
