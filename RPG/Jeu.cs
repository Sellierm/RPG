using DataEntity;
using DataEntity.API;
using DataEntity.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RPG
{
    public class Jeu
    {
        private readonly RPGAPI _api;
        private readonly ISauvegardeService _sauvegardeService;
        private readonly ICombatService _combatService;
        private readonly IMagasinService _magasinService;
        private readonly IAubergeService _aubergeService;
        private readonly IObjetService _objetService;
        private readonly IEnnemiService _ennemiService;
        private readonly RPGContext _context;

        private bool _enCours;
        private Hero? _heroActuel;

        public Jeu()
        {
            _api = RPGAPI.GetInstance();
            _sauvegardeService = _api.GetSauvegardeService();
            _combatService = _api.GetCombatService();
            _magasinService = _api.GetMagasinService();
            _aubergeService = _api.GetAubergeService();
            _objetService = _api.GetObjetService();
            _ennemiService = _api.GetEnnemiService();
            _context = _api.GetContext();
        }

        public void Demarrer()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;//pour un affichage propre des caractères spéciaux
            InitialiserDonnees();
            
            _enCours = true;
            Console.Clear();
            AfficherBanniere("BIENVENUE DANS LE RPG !");

            while (_enCours)
            {
                if (_heroActuel == null)
                {
                    MenuSelectionHero();
                }
                else
                {
                    MenuPrincipal();
                }
            }

            _api.Dispose();
        }

        private void AfficherBanniere(string titre)
        {
            var largeur = Math.Max(titre.Length + 4, 40);
            var bordureHaut = "╔" + new string('═', largeur) + "╗";
            var bordureBas = "╚" + new string('═', largeur) + "╝";
            var espaceTotal = largeur - titre.Length;
            var espaceGauche = espaceTotal / 2;
            var espaceDroit = espaceTotal - espaceGauche;
            var contenu = "║" + new string(' ', espaceGauche) + titre + new string(' ', espaceDroit) + "║";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(bordureHaut);
            Console.WriteLine(contenu);
            Console.WriteLine(bordureBas);
            Console.ResetColor();
        }

        private void AfficherSeparateur()
        {
            Console.WriteLine("═══════════════════════════════════════════");
        }

        private void AttendreTouche()
        {
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }

        private void InitialiserDonnees()
        {
            // S'assurer que la base de données correspond au modèle actuel
            try
            {
                _context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Erreur lors de la création de la base de données : {ex.Message}");
            }

            // Initialiser la carte principale si elle n'existe pas
            if (!_context.CartesDbSet.Any())
            {
                var carte = new Carte("Royaume d'Aventure", "Le monde à explorer");
                _context.CartesDbSet.Add(carte);
                _context.SaveChanges();
            }

            // Initialiser les biomes si nécessaire
            if (!_context.BiomesDbSet.Any())
            {
                var carte = _context.CartesDbSet.First();

                // Biome de départ (Niveau 1+, ennemis jusqu'au niveau 20)
                var plainesVertes = new Biome("Plaines Vertes", "Terres paisibles pour débutants", 1, 20, carte.Id);
                _context.BiomesDbSet.Add(plainesVertes);
                _context.SaveChanges();

                // Biome intermédiaire (Niveau 5+, ennemis jusqu'au niveau 40)
                var foretSombre = new Biome("Forêt Sombre", "Forêt dangereuse avec des créatures hostiles", 5, 40, carte.Id);
                _context.BiomesDbSet.Add(foretSombre);
                _context.SaveChanges();

                // Biome avancé (Niveau 10+, ennemis jusqu'au niveau 60)
                var montagnesMaudites = new Biome("Montagnes Maudites", "Sommets périlleux réservés aux aventuriers expérimentés", 10, 60, carte.Id);
                _context.BiomesDbSet.Add(montagnesMaudites);
                _context.SaveChanges();
            }

            // Initialiser les lieux si nécessaire
            if (!_context.LieuxDbSet.Any())
            {
                var plainesVertes = _context.BiomesDbSet.First(b => b.Nom == "Plaines Vertes");
                var foretSombre = _context.BiomesDbSet.First(b => b.Nom == "Forêt Sombre");

                // Lieux dans les Plaines Vertes
                var auberge1 = new Auberge("Auberge du Voyageur", "Repos et soins");
                var magasin1 = new Magasin("Boutique du Marchand", "Équipements et objets");
                var mairie1 = new Mairie("Mairie", "Sauvegarde");
                
                var villageDepart = new Lieu("Village de Départ", "Un village paisible pour les nouveaux aventuriers", plainesVertes.Id, auberge1, null, magasin1, mairie1, null);
                _context.LieuxDbSet.Add(villageDepart);
                _context.SaveChanges();

                // Lieux dans la Forêt Sombre
                var donjon1 = new Donjon("Donjon Sombre", "Danger extrême", 5);
                
                var campementAventuriers = new Lieu("Campement des Aventuriers", "Base pour explorer la forêt", foretSombre.Id, null, null, null, null, donjon1);
                _context.LieuxDbSet.Add(campementAventuriers);
                _context.SaveChanges();
            }

            // Initialiser des armes
            if (!_context.ArmesDbSet.Any())
            {
                _objetService.AjouterArme(new Arme { Nom = "Épée en Fer", Valeur = 50, IntelligenceRequise = 0, Degats = 15, Type = "Épée", Effet = EffetType.Aucun, EffetValeur = 0, Rarete = ERarete.Commun });
                _objetService.AjouterArme(new Arme { Nom = "Hache de Guerre", Valeur = 100, IntelligenceRequise = 5, Degats = 25, Type = "Hache", Effet = EffetType.Force, EffetValeur = 5, Rarete = ERarete.Rare });
                _objetService.AjouterArme(new Arme { Nom = "Dague Empoisonnée", Valeur = 75, IntelligenceRequise = 3, Degats = 18, Type = "Dague", Effet = EffetType.Aucun, EffetValeur = 0, Rarete = ERarete.PeuCommun });
            }

            // Initialiser des armures
            if (!_context.ArmuresDbSet.Any())
            {
                _objetService.AjouterArmure(new Armure("Armure de Cuir", 40, 0, 5, "Légère", EffetType.Aucun, 0, ERarete.Commun));
                _objetService.AjouterArmure(new Armure("Armure de Plaques", 120, 5, 15, "Lourde", EffetType.Aucun, 0, ERarete.Rare));
                _objetService.AjouterArmure(new Armure("Robe de Mage", 80, 10, 8, "Magique", EffetType.Aucun, 0, ERarete.PeuCommun));
            }

            // Initialiser des consommables
            if (!_context.ConsommablesDbSet.Any())
            {
                _objetService.AjouterConsommable(new Consommable("Potion de Soin", 20, 0, 3, EffetType.Soin, 30, ERarete.Commun));
                _objetService.AjouterConsommable(new Consommable("Élixir de Force", 50, 5, 1, EffetType.Force, 10, ERarete.Rare));
                _objetService.AjouterConsommable(new Consommable("Potion Majeure", 40, 3, 2, EffetType.Soin, 50, ERarete.PeuCommun));
            }

            // Initialiser des ennemis
            if (!_context.EnnemisDbSet.Any())
            {
                _ennemiService.AjouterEnnemi(new Ennemi { Nom = "Gobelin", Description = "Petit et fourbe", PointsDeVie = 40, Force = 8, Energie = 20, Vitesse = 10, Race = ERace.Gobelin, XpValeur = 25, GoldValeur = 15 });
                _ennemiService.AjouterEnnemi(new Ennemi { Nom = "Orc", Description = "Fort et brutal", PointsDeVie = 70, Force = 15, Energie = 30, Vitesse = 5, Race = ERace.Orc, XpValeur = 50, GoldValeur = 30 });
                _ennemiService.AjouterEnnemi(new Ennemi { Nom = "Loup Sauvage", Description = "Rapide et féroce", PointsDeVie = 50, Force = 12, Energie = 40, Vitesse = 15, Race = ERace.Bete, XpValeur = 35, GoldValeur = 20 });
                _ennemiService.AjouterEnnemi(new Ennemi { Nom = "Squelette", Description = "Mort-vivant", PointsDeVie = 60, Force = 10, Energie = 10, Vitesse = 8, Race = ERace.MortVivant, XpValeur = 40, GoldValeur = 25 });
            }
        }

        private void MenuSelectionHero()
        {
            Console.Clear();
            AfficherBanniere("SELECTION DU HEROS");
            Console.WriteLine("1. Charger une partie");
            Console.WriteLine("2. Nouvelle partie");
            Console.WriteLine("3. Quitter");
            AfficherSeparateur();
            Console.Write("Choix : ");

            var choix = Console.ReadLine();
            switch (choix)
            {
                case "1":
                    ChargerPartie();
                    break;
                case "2":
                    NouvellePartie();
                    break;
                case "3":
                    _enCours = false;
                    break;
                default:
                    Console.WriteLine("\n[!] Choix invalide.");
                    AttendreTouche();
                    break;
            }
        }

        private void ChargerPartie()
        {
            Console.Clear();
            var heros = _sauvegardeService.ChargerTous();
            if (heros.Count == 0)
            {
                Console.WriteLine("\n[!] Aucune sauvegarde trouvée.");
                AttendreTouche();
                return;
            }

            AfficherBanniere("SAUVEGARDES DISPONIBLES");
            for (int i = 0; i < heros.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {heros[i].Nom} - Niveau {heros[i].Niveau} - {heros[i].Gold} gold");
            }

            AfficherSeparateur();
            Console.Write("Choisir (0 pour annuler): ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= heros.Count)
            {
                _heroActuel = heros[choix - 1];
                
                // S'assurer que PvMax est défini pour les anciens héros
                if (_heroActuel.PvMax == 0)
                {
                    _heroActuel.PvMax = Math.Max(100, _heroActuel.PointsDeVie);
                }
                
                Console.WriteLine($"\n[OK] Bienvenue {_heroActuel.Nom} !");
                AttendreTouche();
            }
        }

        private void NouvellePartie()
        {
            Console.Clear();
            AfficherBanniere("CREATION DE HEROS");
            Console.Write("Nom de votre héros : ");
            var nom = Console.ReadLine() ?? "Aventurier";

            var premierBiome = _context.BiomesDbSet.Include(b => b.Lieux).OrderBy(b => b.NiveauRequis).First();
            var premierLieu = premierBiome.Lieux.First();

            _heroActuel = new Hero(
                nom: nom,
                description: "Un aventurier courageux",
                pointsDeVie: 100,
                force: 15,
                energie: 100,
                vitesse: 10,
                niveau: 1,
                experience: 0,
                gold: 100,
                lieuActuel: premierLieu,
                batimentActuel: null
            );

            _sauvegardeService.Sauvegarder(_heroActuel);
            Console.WriteLine($"\n[OK] Héros {nom} créé !");
            AttendreTouche();
        }

        private void AfficherStatutHero()
        {
            if (_heroActuel == null) return;

            var largeur = Math.Max(_heroActuel.Nom.Length + 4, 50);
            var bordureHaut = "┌─ " + _heroActuel.Nom + " " + new string('─', largeur - _heroActuel.Nom.Length - 4) + "┐";
            var bordureBas = "└" + new string('─', largeur) + "┘";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(bordureHaut);
            Console.ResetColor();
            
            Console.WriteLine($"│ Niveau: {_heroActuel.Niveau} | PV: {_heroActuel.PointsDeVie}/{_heroActuel.PvMax} | Énergie: {_heroActuel.Energie}/100");
            Console.WriteLine($"│ Gold: {_heroActuel.Gold} | XP: {_heroActuel.Experience}/{_heroActuel.Niveau * 100}");
            
            // Afficher les stats de base
            Console.WriteLine($"│ Force: {_heroActuel.Force} | Vitesse: {_heroActuel.Vitesse}");
            
            // Afficher le lieu actuel
            if (_heroActuel.LieuActuel != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("│ 📍 ");
                Console.ResetColor();
                Console.WriteLine($"Lieu: {_heroActuel.LieuActuel.Nom}");
            }
            
            // Afficher équipement
            if (_heroActuel.ArmeEquipee != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("│ ⚔️ ");
                Console.ResetColor();
                Console.WriteLine($"{_heroActuel.ArmeEquipee.Nom} (Dégâts: +{_heroActuel.ArmeEquipee.Degats})");
            }
            
            if (_heroActuel.ArmureEquipee != null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("│ 🛡️ ");
                Console.ResetColor();
                Console.WriteLine($"{_heroActuel.ArmureEquipee.Nom} (Défense: +{_heroActuel.ArmureEquipee.Defense})");
            }
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(bordureBas);
            Console.ResetColor();
        }

        private void MenuPrincipal()
        {
            Console.Clear();
            AfficherStatutHero();
            
            // Toujours afficher la carte pour permettre la sélection des lieux et biomes
            MenuCarte();
        }

        private void MenuLieu()
        {
            var lieu = _heroActuel!.LieuActuel;
            
            AfficherBanniere($"{lieu.Nom}");
            Console.WriteLine($"{lieu.Description}\n");
            
            Console.WriteLine("=== Bâtiments disponibles ===");
            int optionNum = 1;
            
            if (lieu.Auberge != null)
            {
                Console.WriteLine($"{optionNum}. 🏨 Auberge (Repos/Soin)");
                optionNum++;
            }
            
            if (lieu.Magasin != null)
            {
                Console.WriteLine($"{optionNum}. 🏪 Magasin (Acheter équipement)");
                optionNum++;
            }
            
            if (lieu.Mairie != null)
            {
                Console.WriteLine($"{optionNum}. 🏛️ Mairie (Sauvegarde)");
                optionNum++;
            }
            
            if (lieu.Donjon != null)
            {
                Console.WriteLine($"{optionNum}. 🗡️ Donjon (Combats difficiles)");
                optionNum++;
            }
            
            Console.WriteLine($"{optionNum}. 🗺️ Voyager (Changer de lieu)");
            optionNum++;
            Console.WriteLine($"{optionNum}. 📦 Inventaire");
            optionNum++;
            Console.WriteLine($"{optionNum}. ❌ Quitter le jeu");
            
            AfficherSeparateur();
            Console.Write("Choix : ");

            var choix = Console.ReadLine();
            GererChoixLieu(choix, lieu);
        }

        private void GererChoixLieu(string? choix, Lieu lieu)
        {
            int optionNum = 1;
            
            if (lieu.Auberge != null)
            {
                if (choix == optionNum.ToString())
                {
                    MenuAuberge();
                    return;
                }
                optionNum++;
            }
            
            if (lieu.Magasin != null)
            {
                if (choix == optionNum.ToString())
                {
                    MenuMagasin();
                    return;
                }
                optionNum++;
            }
            
            if (lieu.Mairie != null)
            {
                if (choix == optionNum.ToString())
                {
                    MenuMairie();
                    return;
                }
                optionNum++;
            }
            
            if (lieu.Donjon != null)
            {
                if (choix == optionNum.ToString())
                {
                    MenuDonjon(lieu.Donjon);
                    return;
                }
                optionNum++;
            }
            
            // Voyager (changer de lieu)
            if (choix == optionNum.ToString())
            {
                _heroActuel!.LieuActuel = null; // Sortir du lieu pour revenir à la carte
                return;
            }
            optionNum++;
            
            // Inventaire
            if (choix == optionNum.ToString())
            {
                AfficherInventaire();
                return;
            }
            optionNum++;
            
            // Quitter
            if (choix == optionNum.ToString())
            {
                _sauvegardeService.Sauvegarder(_heroActuel!);
                _enCours = false;
                return;
            }
            
            Console.WriteLine("\n[!] Choix invalide.");
            AttendreTouche();
        }

        private void MenuCarte()
        {
            Console.Clear();
            AfficherStatutHero();
            AfficherBanniere("CARTE DU MONDE");
            
            var carte = _context.CartesDbSet
                .Include(c => c.Biomes)
                    .ThenInclude(b => b.Lieux)
                .FirstOrDefault();
            
            if (carte == null || !carte.Biomes.Any())
            {
                Console.WriteLine("[!] Aucun biome disponible.");
                AttendreTouche();
                return;
            }
            
            Console.WriteLine("=== Biomes et Lieux disponibles ===\n");
            
            var biomes = carte.Biomes.OrderBy(b => b.NiveauRequis).ToList();
            var indexLieux = new Dictionary<int, Lieu>();
            int compteur = 1;
            
            foreach (var biome in biomes)
            {
                // Vérifier si le héros a le niveau requis
                bool accessible = _heroActuel!.Niveau >= biome.NiveauRequis;
                
                Console.ForegroundColor = accessible ? ConsoleColor.Green : ConsoleColor.DarkGray;
                Console.WriteLine($"┌─ {biome.Nom} (Niveau {biome.NiveauRequis}+)");
                Console.ResetColor();
                
                if (!accessible)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"│  ⚠️ Zone verrouillée - Niveau requis: {biome.NiveauRequis}");
                    Console.ResetColor();
                    Console.WriteLine("└" + new string('─', 21));
                    Console.WriteLine();
                    continue;
                }
                
                Console.WriteLine($"│  {biome.Description}");
                Console.WriteLine("│");
                
                var lieux = _context.LieuxDbSet
                    .Include(l => l.Auberge)
                    .Include(l => l.Magasin)
                    .Include(l => l.Mairie)
                    .Include(l => l.Donjon)
                    .Where(l => l.BiomeId == biome.Id)
                    .ToList();
                
                foreach (var lieu in lieux)
                {
                    indexLieux[compteur] = lieu;
                    Console.WriteLine($"│  {compteur}. 📍 {lieu.Nom}");
                    Console.WriteLine($"│     {lieu.Description}");
                    
                    // Afficher les bâtiments disponibles
                    List<string> batiments = new();
                    if (lieu.Auberge != null) batiments.Add("🏨 Auberge");
                    if (lieu.Magasin != null) batiments.Add("🏪 Magasin");
                    if (lieu.Mairie != null) batiments.Add("🏛️ Mairie");
                    if (lieu.Donjon != null) batiments.Add($"🗡️ Donjon (Niv.{lieu.Donjon.NiveauRequis}+)");
                    
                    if (batiments.Any())
                    {
                        Console.WriteLine($"│     ├─ {string.Join(", ", batiments)}");
                    }
                    Console.WriteLine("│");
                    
                    compteur++;
                }
                
                Console.WriteLine("└" + new string('─', 21));
                Console.WriteLine();
            }
            
            Console.WriteLine($"{compteur}. 🗡️ Explorer (Combat aléatoire)");
            Console.WriteLine($"{compteur + 1}. 📦 Inventaire");
            Console.WriteLine($"{compteur + 2}. ❌ Quitter le jeu");
            
            AfficherSeparateur();
            Console.Write("Choisir une destination : ");
            
            if (int.TryParse(Console.ReadLine(), out int choix))
            {
                if (indexLieux.ContainsKey(choix))
                {
                    _heroActuel!.LieuActuel = indexLieux[choix];
                    MenuLieu(); // Accéder au menu du lieu sélectionné
                }
                else if (choix == compteur)
                {
                    MenuExploration();
                }
                else if (choix == compteur + 1)
                {
                    AfficherInventaire();
                }
                else if (choix == compteur + 2)
                {
                    _sauvegardeService.Sauvegarder(_heroActuel!);
                    _enCours = false;
                }
                else
                {
                    Console.WriteLine("\n[!] Choix invalide.");
                    AttendreTouche();
                }
            }
            else
            {
                Console.WriteLine("\n[!] Choix invalide.");
                AttendreTouche();
            }
        }

        private void MenuMairie()
        {
            Console.Clear();
            AfficherStatutHero();
            AfficherBanniere("MAIRIE - SAUVEGARDE");
            
            Console.WriteLine("1. Sauvegarder la partie");
            Console.WriteLine("2. Changer de héros");
            Console.WriteLine("3. Retour");
            
            AfficherSeparateur();
            Console.Write("Choix : ");
            
            var choix = Console.ReadLine();
            switch (choix)
            {
                case "1":
                    _sauvegardeService.Sauvegarder(_heroActuel!);
                    Console.WriteLine("\n✓ Partie sauvegardée !");
                    AttendreTouche();
                    break;
                case "2":
                    _sauvegardeService.Sauvegarder(_heroActuel!);
                    _heroActuel = null;
                    break;
                case "3":
                    break;
                default:
                    Console.WriteLine("\n[!] Choix invalide.");
                    AttendreTouche();
                    break;
            }
        }

        private void MenuDonjon(Donjon donjon)
        {
            Console.Clear();
            AfficherStatutHero();
            AfficherBanniere($"{donjon.Nom}");
            
            if (_heroActuel!.Niveau < donjon.NiveauRequis)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n⚠️ Vous devez être niveau {donjon.NiveauRequis} pour entrer !");
                Console.ResetColor();
                Console.WriteLine($"Votre niveau actuel: {_heroActuel.Niveau}");
                AttendreTouche();
                return;
            }
            
            Console.WriteLine($"{donjon.Description}\n");
            Console.WriteLine("1. Entrer dans le donjon (Combat difficile)");
            Console.WriteLine("2. Retour");
            
            AfficherSeparateur();
            Console.Write("Choix : ");
            
            var choix = Console.ReadLine();
            if (choix == "1")
            {
                // Générer un ennemi plus puissant pour le donjon
                var ennemi = _ennemiService.GenererEnnemiParNiveau(_heroActuel.Niveau + 3);
                Console.WriteLine($"\n⚔️ Un {ennemi.Nom} puissant apparaît !");
                AttendreTouche();

                bool heroEstMort = _combatService.CommencerCombat(_heroActuel, ennemi);
                if (heroEstMort)
                {
                    AfficherGameOver();
                    // Ne pas sauvegarder la partie en cas de game over
                    _heroActuel = null; // Retour au menu principal
                    return;
                }

                // Si le héros est vivant, sauvegarder
                _sauvegardeService.Sauvegarder(_heroActuel);
                AttendreTouche();
            }
        }

        private void MenuExploration()
        {
            Console.Clear();
            AfficherBanniere("EXPLORATION");
            Console.WriteLine("Vous partez à l'aventure...\n");

            // Déterminer le biome actuel
            Biome? biomeActuel = null;
            if (_heroActuel!.LieuActuel != null)
            {
                biomeActuel = _context.BiomesDbSet.FirstOrDefault(b => b.Id == _heroActuel.LieuActuel.BiomeId);
            }

            Ennemi ennemi;
            if (biomeActuel != null)
            {
                ennemi = _ennemiService.GenererEnnemiPourBiome(_heroActuel.Niveau, biomeActuel.NiveauMaxEnnemi);
                Console.WriteLine($"Vous explorez {biomeActuel.Nom}...");
            }
            else
            {
                biomeActuel = _context.BiomesDbSet
                    .Where(b => b.NiveauRequis <= _heroActuel.Niveau)
                    .OrderBy(b => b.NiveauRequis)
                    .FirstOrDefault();

                if (biomeActuel != null)
                {
                    ennemi = _ennemiService.GenererEnnemiPourBiome(_heroActuel.Niveau, biomeActuel.NiveauMaxEnnemi);
                }
                else
                {
                    ennemi = _ennemiService.GenererEnnemiParNiveau(_heroActuel.Niveau);
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[!] Un {ennemi.Nom} apparaît !");
            Console.ResetColor();

            AttendreTouche();

            bool heroEstMort = _combatService.CommencerCombat(_heroActuel!, ennemi);
            if (heroEstMort)
            {
                AfficherGameOver();
                _heroActuel = null; // Retour au menu principal
                return;
            }

            _sauvegardeService.Sauvegarder(_heroActuel!);
            AttendreTouche();
        }

        private void AfficherGameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string titre = "GAME OVER";
            AfficherBanniere(titre);
            Console.ResetColor();

            Console.WriteLine("\nVous avez été vaincu...");
            Console.WriteLine("La partie n'a pas été sauvegardée.");
            AttendreTouche();
        }

        private void MenuAuberge()
        {
            bool dansAuberge = true;
            while (dansAuberge)
            {
                Console.Clear();
                AfficherStatutHero();
                _aubergeService.AfficherMenu(_heroActuel!);
                var choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        _aubergeService.Soigner(_heroActuel!);
                        AttendreTouche();
                        break;
                    case "2":
                        _aubergeService.Reposer(_heroActuel!);
                        AttendreTouche();
                        break;
                    case "3":
                        dansAuberge = false;
                        break;
                    default:
                        Console.WriteLine("\n[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private void MenuMagasin()
        {
            bool dansMagasin = true;
            while (dansMagasin)
            {
                Console.Clear();
                AfficherStatutHero();
                AfficherBanniere($"MAGASIN - Gold: {_heroActuel!.Gold}");
                Console.WriteLine("1. Voir les armes");
                Console.WriteLine("2. Voir les armures");
                Console.WriteLine("3. Voir les consommables");
                Console.WriteLine("4. Quitter");
                AfficherSeparateur();
                Console.Write("Choix : ");

                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AcheterArme();
                        break;
                    case "2":
                        AcheterArmure();
                        break;
                    case "3":
                        AcheterConsommable();
                        break;
                    case "4":
                        dansMagasin = false;
                        break;
                    default:
                        Console.WriteLine("\n[!] Choix invalide.");
                        AttendreTouche();
                        break;
                }
            }
        }

        private void AcheterArme()
        {
            Console.Clear();
            var armes = _magasinService.GetArmesDisponibles();
            AfficherBanniere("ARMES DISPONIBLES");
            
            for (int i = 0; i < armes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {armes[i].Nom.PadRight(20)} | Dégâts: {armes[i].Degats.ToString().PadLeft(2)} | {armes[i].Valeur}g | {armes[i].Rarete}");
            }

            AfficherSeparateur();
            Console.Write("Acheter (0 pour annuler): ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= armes.Count)
            {
                _magasinService.AcheterArme(_heroActuel!, armes[choix - 1]);
            }
            AttendreTouche();
        }

        private void AcheterArmure()
        {
            Console.Clear();
            var armures = _magasinService.GetArmuresDisponibles();
            AfficherBanniere("ARMURES DISPONIBLES");
            
            for (int i = 0; i < armures.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {armures[i].Nom.PadRight(20)} | Défense: {armures[i].Defense.ToString().PadLeft(2)} | {armures[i].Valeur}g | {armures[i].Rarete}");
            }

            AfficherSeparateur();
            Console.Write("Acheter (0 pour annuler): ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= armures.Count)
            {
                _magasinService.AcheterArmure(_heroActuel!, armures[choix - 1]);
            }
            AttendreTouche();
        }

        private void AcheterConsommable()
        {
            Console.Clear();
            var consommables = _magasinService.GetConsommablesDisponibles();
            AfficherBanniere("CONSOMMABLES DISPONIBLES");
            
            for (int i = 0; i < consommables.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {consommables[i].Nom.PadRight(20)} | {consommables[i].Effet} (+{consommables[i].EffetValeur}) | {consommables[i].Valeur}g");
            }

            AfficherSeparateur();
            Console.Write("Acheter (0 pour annuler): ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= consommables.Count)
            {
                _magasinService.AcheterConsommable(_heroActuel!, consommables[choix - 1]);
            }
            AttendreTouche();
        }

        private void AfficherInventaire()
        {
            Console.Clear();
            AfficherStatutHero();
            AfficherBanniere("INVENTAIRE");

            Console.WriteLine("\n--- ARMES ---");
            if (_heroActuel!.InventaireArme.Count == 0)
            {
                Console.WriteLine("(vide)");
            }
            else
            {
                for (int i = 0; i < _heroActuel.InventaireArme.Count; i++)
                {
                    var arme = _heroActuel.InventaireArme[i];
                    var equipee = arme == _heroActuel.ArmeEquipee ? " [EQUIPEE]" : "";
                    Console.WriteLine($"{i + 1}. {arme.Nom.PadRight(25)} | Dégâts: {arme.Degats}{equipee}");
                }

                Console.Write("\nÉquiper une arme (0 pour passer): ");
                if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= _heroActuel.InventaireArme.Count)
                {
                    _heroActuel.ArmeEquipee = _heroActuel.InventaireArme[choix - 1];
                    Console.WriteLine($"\n[OK] {_heroActuel.ArmeEquipee.Nom} équipée !");
                }
            }

            Console.WriteLine("\n--- ARMURES ---");
            if (_heroActuel.InventaireArmure.Count == 0)
            {
                Console.WriteLine("(vide)");
            }
            else
            {
                for (int i = 0; i < _heroActuel.InventaireArmure.Count; i++)
                {
                    var armure = _heroActuel.InventaireArmure[i];
                    var equipee = armure == _heroActuel.ArmureEquipee ? " [EQUIPEE]" : "";
                    Console.WriteLine($"{i + 1}. {armure.Nom.PadRight(25)} | Défense: {armure.Defense}{equipee}");
                }

                Console.Write("\nÉquiper une armure (0 pour passer): ");
                if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= _heroActuel.InventaireArmure.Count)
                {
                    _heroActuel.ArmureEquipee = _heroActuel.InventaireArmure[choix - 1];
                    Console.WriteLine($"\n[OK] {_heroActuel.ArmureEquipee.Nom} équipée !");
                }
            }

            Console.WriteLine("\n--- CONSOMMABLES ---");
            if (_heroActuel.InventaireConsommable.Count == 0)
            {
                Console.WriteLine("(vide)");
            }
            else
            {
                foreach (var conso in _heroActuel.InventaireConsommable)
                {
                    Console.WriteLine($"- {conso.Nom.PadRight(25)} | {conso.Effet} (+{conso.EffetValeur}) x{conso.NombreUtilisation}");
                }
            }

            AfficherSeparateur();
            AttendreTouche();
        }
    }
}