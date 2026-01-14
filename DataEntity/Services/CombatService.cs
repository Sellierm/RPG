using DataEntity.Interfaces;

namespace DataEntity.Services
{
    public class CombatService : ICombatService
    {
        private readonly RPGContext _context;
        private readonly Random _random;

        public CombatService(RPGContext context)
        {
            _context = context;
            _random = new Random();
        }

        // Lance et gère un combat complet entre le héros et un ennemi
        // Retourne true si le héros est mort (game over), false sinon
        public bool CommencerCombat(Hero hero, Ennemi ennemi)
        {
            Console.Clear();
            AfficherBanniereCombat();
            Console.WriteLine($"\n{hero.Nom} affronte {ennemi.Nom} !");
            Console.WriteLine($"{ennemi.Description}\n");
            
            Console.WriteLine("Appuyez sur une touche pour commencer le combat...");
            Console.ReadKey(true);

            // Vérifier si le héros est déjà mort avant le combat
            if (hero.PointsDeVie <= 0)
            {
                Console.WriteLine($"\n[!] {hero.Nom} n'a plus de points de vie !");
                return true;
            }

            // Stocker les PV max de l'ennemi
            int ennemiPvMax = ennemi.PointsDeVie;

            int tour = 1;
            while (hero.PointsDeVie > 0 && ennemi.PointsDeVie > 0)
            {
                Console.Clear();
                AfficherBanniereCombat();
                Console.WriteLine($"\n=== TOUR {tour} ===\n");
                
                // Afficher les barres de vie avec les vrais PV max
                AfficherBarreDeVie(hero.Nom, hero.PointsDeVie, hero.PvMax, ConsoleColor.Green);
                AfficherBarreDeVie(ennemi.Nom, ennemi.PointsDeVie, ennemiPvMax, ConsoleColor.Red);
                
                Console.WriteLine("\n--- Actions disponibles ---");
                Console.WriteLine("1. Attaquer");
                Console.WriteLine("2. Utiliser un consommable");
                Console.WriteLine("3. Fuir (50% de chance)");
                Console.Write("\nVotre choix : ");

                var choix = Console.ReadLine();
                Console.WriteLine();

                bool tourIncremente = true; // Par défaut, le tour passe

                switch (choix)
                {
                    case "1":
                        // Héros attaque en premier si plus rapide
                        if (hero.Vitesse >= ennemi.Vitesse)
                        {
                            if (Attaquer(hero, ennemi))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(">>> VICTOIRE ! <<<");
                                Console.ResetColor();
                                GagnerCombat(hero, ennemi);
                                return false; // héros victorieux
                            }
                            
                            if (ennemi.PointsDeVie > 0 && Attaquer(ennemi, hero))
                            {
                                // héros mort
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(">>> DEFAITE ! <<<");
                                Console.ResetColor();
                                Console.WriteLine($"\n{hero.Nom} a été vaincu...");
                                AttendreTouche();
                                return true;
                            }
                        }
                        else
                        {
                            // Ennemi attaque en premier
                            if (Attaquer(ennemi, hero))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(">>> DEFAITE ! <<<");
                                Console.ResetColor();
                                Console.WriteLine($"\n{hero.Nom} a été vaincu...");
                                AttendreTouche();
                                return true;
                            }
                            
                            if (hero.PointsDeVie > 0 && Attaquer(hero, ennemi))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(">>> VICTOIRE ! <<<");
                                Console.ResetColor();
                                GagnerCombat(hero, ennemi);
                                return false;
                            }
                        }
                        break;

                    case "2":
                        tourIncremente = UtiliserConsommable(hero);
                        // Ennemi attaque seulement si un consommable a été utilisé
                        if (tourIncremente && ennemi.PointsDeVie > 0 && Attaquer(ennemi, hero))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(">>> DEFAITE ! <<<");
                            Console.ResetColor();
                            Console.WriteLine($"\n{hero.Nom} a été vaincu...");
                            AttendreTouche();
                            return true;
                        }
                        break;

                    case "3":
                        if (_random.Next(0, 100) < 50)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{hero.Nom} a réussi à fuir !");
                            Console.ResetColor();
                            AttendreTouche();
                            return false; // fuite -> pas de mort
                        }
                        else
                        {
                            Console.WriteLine($"{hero.Nom} n'a pas réussi à fuir !");
                            // Ennemi attaque après échec de fuite
                            if (Attaquer(ennemi, hero))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(">>> DEFAITE ! <<<");
                                Console.ResetColor();
                                Console.WriteLine($"\n{hero.Nom} a été vaincu...");
                                AttendreTouche();
                                return true;
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Action invalide !");
                        tourIncremente = false; // Ne pas compter le tour si action invalide
                        break;
                }

                if (tourIncremente)
                {
                    tour++;
                }

                AttendreTouche();
            }

            // Si boucle sortie par condition (sécurisé)
            return hero.PointsDeVie <= 0;
        }

        // Affiche une bannière décorative pour le combat
        private void AfficherBanniereCombat()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string titre = "COMBAT !";
            int largeur = Math.Max(titre.Length + 4, 34);
            Console.WriteLine("╔" + new string('═', largeur) + "╗");
            Console.WriteLine($"║  {titre.PadRight(largeur - 2)}║");
            Console.WriteLine("╚" + new string('═', largeur) + "╝");
            Console.ResetColor();
        }

        // Affiche une barre de vie visuelle avec des caractères ASCII
        // Montre les PV actuels par rapport aux PV maximum
        private void AfficherBarreDeVie(string nom, int pvActuels, int pvMax, ConsoleColor couleur)
        {
            const int largeurBarre = 30;
            
            // S'assurer que pvMax est au moins égal à pvActuels si pvActuels est positif
            if (pvMax < pvActuels && pvActuels > 0)
            {
                pvMax = pvActuels;
            }
            
            int pourcentage = pvMax > 0 ? Math.Max(0, Math.Min(100, (pvActuels * 100) / pvMax)) : 0;
            int barresRemplies = (pourcentage * largeurBarre) / 100;

            Console.Write($"{nom.PadRight(20)} ");
            Console.ForegroundColor = couleur;
            Console.Write("[");
            Console.Write(new string('#', barresRemplies));
            Console.Write(new string('.', largeurBarre - barresRemplies));
            Console.Write("]");
            Console.ResetColor();
            Console.WriteLine($" {pvActuels}/{pvMax} PV ({pourcentage}%)");
        }

        // Permet au héros d'utiliser un consommable pendant le combat
        // Retourne true si un consommable a été utilisé (le tour passe)
        private bool UtiliserConsommable(Hero hero)
        {
            if (hero.InventaireConsommable.Count == 0)
            {
                Console.WriteLine("Aucun consommable disponible !");
                return false; // Aucun consommable, tour ne passe pas
            }

            Console.WriteLine("\n--- Consommables disponibles ---");
            for (int i = 0; i < hero.InventaireConsommable.Count; i++)
            {
                var conso = hero.InventaireConsommable[i];
                Console.WriteLine($"{i + 1}. {conso.Nom} - {conso.Effet} (+{conso.EffetValeur}) x{conso.NombreUtilisation}");
            }

            Console.Write("\nChoisir (0 pour annuler): ");
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= hero.InventaireConsommable.Count)
            {
                var consommable = hero.InventaireConsommable[choix - 1];
                
                switch (consommable.Effet)
                {
                    case EffetType.Soin:
                        hero.PointsDeVie = Math.Min(hero.PvMax, hero.PointsDeVie + consommable.EffetValeur);
                        Console.WriteLine($"\n{hero.Nom} utilise {consommable.Nom} et recupere {consommable.EffetValeur} PV !");
                        break;
                    case EffetType.Force:
                        hero.Force += consommable.EffetValeur;
                        Console.WriteLine($"\n{hero.Nom} utilise {consommable.Nom} et gagne {consommable.EffetValeur} Force !");
                        break;
                    default:
                        Console.WriteLine($"\n{hero.Nom} utilise {consommable.Nom} !");
                        break;
                }

                consommable.NombreUtilisation--;
                if (consommable.NombreUtilisation <= 0)
                {
                    hero.InventaireConsommable.Remove(consommable);
                }
                return true; // Consommable utilisé, tour passe
            }
            else
            {
                // Choix invalide ou annulé
                return false; // Tour ne passe pas
            }
        }

        // Gère la victoire du héros après un combat
        // Donne XP et or, vérifie la montée de niveau
        private void GagnerCombat(Hero hero, Ennemi ennemi)
        {
            Console.WriteLine($"\n{ennemi.Nom} estvaincu !");
            Console.WriteLine($"\n[+] {ennemi.XpValeur} XP");
            Console.WriteLine($"[+] {ennemi.GoldValeur} Gold");
            
            hero.Gold += ennemi.GoldValeur;
            GagnerExperience(hero, ennemi.XpValeur);
            
            AttendreTouche();
        }

        // Calcule et applique les dégâts d'une attaque
        // Prend en compte force, équipement, défense, et coups critiques
        // Retourne true si le défenseur est mort
        public bool Attaquer(Personnage attaquant, Personnage defenseur)
        {
            // Calculer les dégâts
            int degatsBase = attaquant.Force;
            
            // Ajouter dégâts d'arme si c'est un héros
            if (attaquant is Hero hero && hero.ArmeEquipee != null)
            {
                degatsBase += hero.ArmeEquipee.Degats;
            }

            // Soustraire la défense si c'est un héros défenseur
            int degatsInfliges = degatsBase;
            if (defenseur is Hero heroDefenseur && heroDefenseur.ArmureEquipee != null)
            {
                degatsInfliges = Math.Max(1, degatsBase - heroDefenseur.ArmureEquipee.Defense);
            }

            // Variabilité (80% à 120%)
            degatsInfliges = _random.Next((int)(degatsInfliges * 0.8), (int)(degatsInfliges * 1.2) + 1);

            // Coup critique (10% de chance, x2 dégâts)
            bool coupCritique = _random.Next(0, 100) < 10;
            if (coupCritique)
            {
                degatsInfliges *= 2;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($">>> COUP CRITIQUE ! <<<");
                Console.ResetColor();
            }

            defenseur.PointsDeVie -= degatsInfliges;

            // Affichage de l'attaque
            Console.ForegroundColor = attaquant is Hero ? ConsoleColor.Cyan : ConsoleColor.Red;
            Console.Write($"{attaquant.Nom}");
            Console.ResetColor();
            Console.Write(" attaque ");
            Console.ForegroundColor = defenseur is Hero ? ConsoleColor.Cyan : ConsoleColor.Red;
            Console.Write($"{defenseur.Nom}");
            Console.ResetColor();
            Console.WriteLine($" et inflige {degatsInfliges} dégâts !");

            return defenseur.PointsDeVie <= 0;
        }

        // Ajoute de l'expérience au héros et affiche le gain
        // Déclenche automatiquement la vérification de montée de niveau
        public void GagnerExperience(Hero hero, int xp)
        {
            hero.Experience += xp;
            Console.WriteLine($"\n{hero.Nom} gagne {xp} points d'expérience !");
            VerifierMonteeNiveau(hero);
        }

        // Vérifie si le héros a assez d'XP pour monter de niveau
        // Gère la montée de niveau, les points de compétence et la restauration
        public void VerifierMonteeNiveau(Hero hero)
        {
            int xpRequis = hero.Niveau * 100;
            
            while (hero.Experience >= xpRequis)
            {
                hero.Experience -= xpRequis;
                hero.Niveau++;
                hero.PointsCompetence += 5; // Ajouter 5 points de compétence
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                string titre = "LEVEL UP !";
                int largeur = Math.Max(titre.Length + 4, 34);
                Console.WriteLine("╔" + new string('═', largeur) + "╗");
                Console.WriteLine($"║  {titre.PadRight(largeur - 2)}║");
                Console.WriteLine("╚" + new string('═', largeur) + "╝");
                Console.ResetColor();
                Console.WriteLine($"\n🌟 {hero.Nom} passe au niveau {hero.Niveau} ! 🌟");
                Console.WriteLine($"\n[+] Vous avez gagné 5 points de compétence !");
                
                // Restauration complète
                hero.PointsDeVie = hero.PvMax; // Restaurer aux PV max réels
                hero.Energie = Math.Max(hero.Energie, 100); // Restaurer l'énergie au minimum 100
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✓ Santé et énergie restaurées !");
                Console.ResetColor();
                
                AttendreTouche();
                
                // Distribution immédiate des points de compétence
                DistribuerPointsCompetenceImmediat(hero);
                
                xpRequis = hero.Niveau * 100;
            }
            
            // Plus d'AttendreTouche ici - sera géré par le combat
        }

        // Permet au joueur de distribuer ses points de compétence
        // Appelé automatiquement après chaque montée de niveau
        private void DistribuerPointsCompetenceImmediat(Hero hero)
        {
            while (hero.PointsCompetence > 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                string titre = "DISTRIBUTION DES POINTS";
                int largeur = Math.Max(titre.Length + 4, 34);
                Console.WriteLine("╔" + new string('═', largeur) + "╗");
                Console.WriteLine($"║  {titre.PadRight(largeur - 2)}║");
                Console.WriteLine("╚" + new string('═', largeur) + "╝");
                Console.ResetColor();
                
                Console.WriteLine($"\nPoints disponibles : {hero.PointsCompetence}");
                Console.WriteLine();
                Console.WriteLine($"Stats actuelles de {hero.Nom} :");
                Console.WriteLine($"1. Force     : {hero.Force} (+1 par point)");
                Console.WriteLine($"2. Vitesse   : {hero.Vitesse} (+1 par point)");
                Console.WriteLine($"3. PV Max    : {hero.PvMax} (+10 par point)");
                Console.WriteLine($"4. Énergie Max : {hero.Energie} (+10 par point)");
                Console.WriteLine();
                Console.WriteLine("5. Garder les points pour plus tard");
                Console.WriteLine();
                Console.Write("Améliorer quelle statistique ? ");

                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        hero.Force++;
                        hero.PointsCompetence--;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n[+] Force augmentée ! (maintenant : {hero.Force})");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1000);
                        break;
                    case "2":
                        hero.Vitesse++;
                        hero.PointsCompetence--;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n[+] Vitesse augmentée ! (maintenant : {hero.Vitesse})");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1000);
                        break;
                    case "3":
                        hero.PvMax += 10;
                        hero.PointsDeVie += 10; // Augmenter aussi les PV actuels pour maintenir la cohérence
                        hero.PointsCompetence--;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n[+] PV Max augmentés ! (maintenant : {hero.PvMax})");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1000);
                        break;
                    case "4":
                        hero.Energie += 10;
                        hero.PointsCompetence--;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n[+] Énergie Max augmentée ! (maintenant : {hero.Energie})");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1000);
                        break;
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\n[OK] Points de compétence sauvegardés pour plus tard !");
                        Console.ResetColor();
                        return; // Sortir sans AttendreTouche
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] Choix invalide.");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(800);
                        break;
                }
            }

            // Message final seulement si tous les points ont été distribués
            if (hero.PointsCompetence == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[OK] Tous les points ont été distribués !");
                Console.ResetColor();
                System.Threading.Thread.Sleep(1500); // Laisser le temps de lire
            }
        }

        // Génère un ennemi aléatoire basé sur un template
        // Ajuste les stats de l'ennemi selon le niveau du héros
        public Ennemi GenererEnnemiAleatoire(int niveauHero)
        {
            var ennemis = _context.Set<Ennemi>().ToList();
            if (ennemis.Any())
            {
                var modele = ennemis[_random.Next(ennemis.Count)];
                return new Ennemi
                {
                    Nom = $"{modele.Nom} (Niv.{niveauHero})",
                    Description = modele.Description,
                    PointsDeVie = modele.PointsDeVie + (niveauHero * 10),
                    Force = modele.Force + (niveauHero * 2),
                    Energie = modele.Energie,
                    Vitesse = modele.Vitesse + niveauHero,
                    Race = modele.Race,
                    XpValeur = niveauHero * 25,
                    GoldValeur = niveauHero * 15
                };
            }

            return new Ennemi
            {
                Nom = $"Créature (Niv.{niveauHero})",
                Description = "Une créature hostile",
                PointsDeVie = 50 + (niveauHero * 10),
                Force = 10 + (niveauHero * 3),
                Energie = 50,
                Vitesse = 5 + (niveauHero * 2),
                Race = ERace.Humain,
                XpValeur = niveauHero * 25,
                GoldValeur = niveauHero * 10
            };
        }

        // Pause pour que le joueur puisse lire les messages
        private void AttendreTouche()
        {
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }
    }
}
