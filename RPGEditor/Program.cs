using DataEntity;
using DataEntity.API;
using RPGEditor.Services;

namespace RPGEditor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            using var context = new RPGContext();
            var api = RPGAPI.GetInstance();
            
            var armeService = new ArmeService(context);
            var armureService = new ArmureService(context);
            var consommableService = new ConsommableService(context);
            var ennemiService = new EnnemiService(context);
            var carteService = api.GetCarteService();
            var biomeEditorService = new BiomeEditorService(carteService, context);
            var lieuEditorService = new LieuEditorService(carteService, context);

            while (true)
            {
                Console.Clear();
                AfficherBanniere("EDITEUR RPG - MENU PRINCIPAL");
                Console.WriteLine("1. Gestion des Armes");
                Console.WriteLine("2. Gestion des Armures");
                Console.WriteLine("3. Gestion des Consommables");
                Console.WriteLine("4. Gestion des Ennemis");
                Console.WriteLine("5. Gestion des Biomes");
                Console.WriteLine("6. Gestion des Lieux");
                Console.WriteLine("7. Quitter");
                AfficherSeparateur();
                Console.Write("Choix : ");

                if (!int.TryParse(Console.ReadLine(), out int choixPrincipal))
                {
                    Console.WriteLine("[!] Choix invalide.");
                    AttendreTouche();
                    continue;
                }

                switch (choixPrincipal)
                {
                    case 1:
                        MenuArmes(armeService);
                        break;
                    case 2:
                        MenuArmures(armureService);
                        break;
                    case 3:
                        MenuConsommables(consommableService);
                        break;
                    case 4:
                        MenuEnnemis(ennemiService);
                        break;
                    case 5:
                        MenuBiomes(biomeEditorService);
                        break;
                    case 6:
                        MenuLieux(lieuEditorService);
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private static void MenuArmes(ArmeService service)
        {
            while (true)
            {
                Console.Clear();
                AfficherBanniere("GESTION DES ARMES");
                Console.WriteLine("1. Créer");
                Console.WriteLine("2. Lire");
                Console.WriteLine("3. Modifier");
                Console.WriteLine("4. Supprimer");
                Console.WriteLine("5. Retour");
                AfficherSeparateur();
                Console.Write("Choix : ");

                if (!int.TryParse(Console.ReadLine(), out int choix))
                {
                    Console.WriteLine("[!] Choix invalide.");
                    AttendreTouche();
                    continue;
                }

                switch (choix)
                {
                    case 1:
                        service.CreateArme();
                        AttendreTouche();
                        break;
                    case 2:
                        service.ListArmes();
                        AttendreTouche();
                        break;
                    case 3:
                        service.UpdateArme();
                        AttendreTouche();
                        break;
                    case 4:
                        service.DeleteArme();
                        AttendreTouche();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private static void MenuArmures(ArmureService service)
        {
            while (true)
            {
                Console.Clear();
                AfficherBanniere("GESTION DES ARMURES");
                Console.WriteLine("1. Créer");
                Console.WriteLine("2. Lire");
                Console.WriteLine("3. Modifier");
                Console.WriteLine("4. Supprimer");
                Console.WriteLine("5. Retour");
                AfficherSeparateur();
                Console.Write("Choix : ");

                if (!int.TryParse(Console.ReadLine(), out int choix))
                {
                    Console.WriteLine("[!] Choix invalide.");
                    AttendreTouche();
                    continue;
                }

                switch (choix)
                {
                    case 1:
                        service.CreateArmure();
                        AttendreTouche();
                        break;
                    case 2:
                        service.ListArmures();
                        AttendreTouche();
                        break;
                    case 3:
                        service.UpdateArmure();
                        AttendreTouche();
                        break;
                    case 4:
                        service.DeleteArmure();
                        AttendreTouche();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private static void MenuConsommables(ConsommableService service)
        {
            while (true)
            {
                Console.Clear();
                AfficherBanniere("GESTION DES CONSOMMABLES");
                Console.WriteLine("1. Créer");
                Console.WriteLine("2. Lire");
                Console.WriteLine("3. Modifier");
                Console.WriteLine("4. Supprimer");
                Console.WriteLine("5. Retour");
                AfficherSeparateur();
                Console.Write("Choix : ");

                if (!int.TryParse(Console.ReadLine(), out int choix))
                {
                    Console.WriteLine("[!] Choix invalide.");
                    AttendreTouche();
                    continue;
                }

                switch (choix)
                {
                    case 1:
                        service.CreateConsommable();
                        AttendreTouche();
                        break;
                    case 2:
                        service.ListConsommables();
                        AttendreTouche();
                        break;
                    case 3:
                        service.UpdateConsommable();
                        AttendreTouche();
                        break;
                    case 4:
                        service.DeleteConsommable();
                        AttendreTouche();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private static void MenuEnnemis(EnnemiService service)
        {
            while (true)
            {
                Console.Clear();
                AfficherBanniere("GESTION DES ENNEMIS");
                Console.WriteLine("1. Créer");
                Console.WriteLine("2. Lire");
                Console.WriteLine("3. Modifier");
                Console.WriteLine("4. Supprimer");
                Console.WriteLine("5. Retour");
                AfficherSeparateur();
                Console.Write("Choix : ");

                if (!int.TryParse(Console.ReadLine(), out int choix))
                {
                    Console.WriteLine("[!] Choix invalide.");
                    AttendreTouche();
                    continue;
                }

                switch (choix)
                {
                    case 1:
                        service.CreateEnnemi();
                        AttendreTouche();
                        break;
                    case 2:
                        service.ListEnnemis();
                        AttendreTouche();
                        break;
                    case 3:
                        service.UpdateEnnemi();
                        AttendreTouche();
                        break;
                    case 4:
                        service.DeleteEnnemi();
                        AttendreTouche();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private static void MenuBiomes(BiomeEditorService service)
        {
            while (true)
            {
                Console.Clear();
                AfficherBanniere("GESTION DES BIOMES");
                Console.WriteLine("1. Créer");
                Console.WriteLine("2. Lire");
                Console.WriteLine("3. Modifier");
                Console.WriteLine("4. Supprimer");
                Console.WriteLine("5. Retour");
                AfficherSeparateur();
                Console.Write("Choix : ");

                if (!int.TryParse(Console.ReadLine(), out int choix))
                {
                    Console.WriteLine("[!] Choix invalide.");
                    AttendreTouche();
                    continue;
                }

                switch (choix)
                {
                    case 1:
                        service.CreateBiome();
                        AttendreTouche();
                        break;
                    case 2:
                        service.ListBiomes();
                        AttendreTouche();
                        break;
                    case 3:
                        service.UpdateBiome();
                        AttendreTouche();
                        break;
                    case 4:
                        service.DeleteBiome();
                        AttendreTouche();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private static void MenuLieux(LieuEditorService service)
        {
            while (true)
            {
                Console.Clear();
                AfficherBanniere("GESTION DES LIEUX");
                Console.WriteLine("1. Créer");
                Console.WriteLine("2. Lire");
                Console.WriteLine("3. Modifier");
                Console.WriteLine("4. Supprimer");
                Console.WriteLine("5. Retour");
                AfficherSeparateur();
                Console.Write("Choix : ");

                if (!int.TryParse(Console.ReadLine(), out int choix))
                {
                    Console.WriteLine("[!] Choix invalide.");
                    AttendreTouche();
                    continue;
                }

                switch (choix)
                {
                    case 1:
                        service.CreateLieu();
                        AttendreTouche();
                        break;
                    case 2:
                        service.ListLieux();
                        AttendreTouche();
                        break;
                    case 3:
                        service.UpdateLieu();
                        AttendreTouche();
                        break;
                    case 4:
                        service.DeleteLieu();
                        AttendreTouche();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private static void AfficherBanniere(string titre)
        {
            int largeur = Math.Max(titre.Length + 4, 34);
            Console.WriteLine("╔" + new string('═', largeur) + "╗");
            Console.WriteLine($"║  {titre.PadRight(largeur - 2)}║");
            Console.WriteLine("╚" + new string('═', largeur) + "╝");
        }

        private static void AfficherSeparateur()
        {
            Console.WriteLine("\n" + new string('─', 50) + "\n");
        }

        private static void AttendreTouche()
        {
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
    }
}
