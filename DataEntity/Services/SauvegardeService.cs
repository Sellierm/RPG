using DataEntity.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DataEntity.Services
{
    public class SauvegardeService : ISauvegardeService
    {
        private const string DATABASE_PATH = "../../../../Database/";
        private readonly RPGContext _context;

        public SauvegardeService(RPGContext context)
        {
            _context = context;
            
            // Créer le dossier s'il n'existe pas
            if (!Directory.Exists(DATABASE_PATH))
            {
                Directory.CreateDirectory(DATABASE_PATH);
            }
        }

        public void Sauvegarder(Hero hero)
        {
            if (hero == null)
            {
                Console.WriteLine("Aucun héros à sauvegarder.");
                return;
            }

            // Créer un objet de sauvegarde simplifié sans les proxies EF
            var heroSauvegarde = new
            {
                hero.Nom,
                hero.Description,
                hero.Niveau,
                hero.Experience,
                hero.Gold,
                hero.PointsDeVie,
                hero.PvMax, // Inclure les PV max dans la sauvegarde
                hero.Force,
                hero.Energie,
                hero.Vitesse,
                hero.PointsCompetence,
                
                // Sauvegarder seulement l'ID du lieu actuel
                LieuActuelId = hero.LieuActuel?.Id,
                
                // Sauvegarder les armes (sans proxies)
                InventaireArme = hero.InventaireArme.Select(a => new
                {
                    a.Nom,
                    a.Valeur,
                    a.IntelligenceRequise,
                    a.Degats,
                    a.Type,
                    a.Effet,
                    a.EffetValeur,
                    a.Rarete
                }).ToList(),
                
                // Sauvegarder l'arme équipée
                ArmeEquipee = hero.ArmeEquipee != null ? new
                {
                    hero.ArmeEquipee.Nom,
                    hero.ArmeEquipee.Valeur,
                    hero.ArmeEquipee.IntelligenceRequise,
                    hero.ArmeEquipee.Degats,
                    hero.ArmeEquipee.Type,
                    hero.ArmeEquipee.Effet,
                    hero.ArmeEquipee.EffetValeur,
                    hero.ArmeEquipee.Rarete
                } : null,
                
                // Sauvegarder les armures (sans proxies)
                InventaireArmure = hero.InventaireArmure.Select(a => new
                {
                    a.Nom,
                    a.Valeur,
                    a.IntelligenceRequise,
                    a.Defense,
                    a.Effet,
                    a.EffetValeur,
                    a.Rarete
                }).ToList(),
                
                // Sauvegarder l'armure équipée
                ArmureEquipee = hero.ArmureEquipee != null ? new
                {
                    hero.ArmureEquipee.Nom,
                    hero.ArmureEquipee.Valeur,
                    hero.ArmureEquipee.IntelligenceRequise,
                    hero.ArmureEquipee.Defense,
                    hero.ArmureEquipee.Effet,
                    hero.ArmureEquipee.EffetValeur,
                    hero.ArmureEquipee.Rarete
                } : null,
                
                // Sauvegarder les consommables (sans proxies)
                InventaireConsommable = hero.InventaireConsommable.Select(c => new
                {
                    c.Nom,
                    c.Valeur,
                    c.IntelligenceRequise,
                    c.NombreUtilisation,
                    c.Effet,
                    c.EffetValeur,
                    c.Rarete
                }).ToList()
            };

            var json = JsonConvert.SerializeObject(heroSauvegarde, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            File.WriteAllText($"{DATABASE_PATH}{hero.Nom}.json", json);
            Console.WriteLine($"[OK] Partie de {hero.Nom} sauvegardée dans Database/{hero.Nom}.json !");
        }

        public Hero? Charger(string nomHero)
        {
            var filePath = $"{DATABASE_PATH}{nomHero}.json";
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Aucune sauvegarde trouvée pour {nomHero}.");
                return null;
            }

            try
            {
                var json = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<dynamic>(json);

                if (data == null)
                {
                    Console.WriteLine($"Erreur lors de la désérialisation de {nomHero}.");
                    return null;
                }

                // Récupérer le lieu depuis la base de données
                Lieu? lieuActuel = null;
                if (data.LieuActuelId != null)
                {
                    int lieuId = (int)data.LieuActuelId;
                    lieuActuel = _context.LieuxDbSet.FirstOrDefault(l => l.Id == lieuId);
                }

                // Si pas de lieu trouvé, prendre le premier lieu disponible
                if (lieuActuel == null)
                {
                    var premierBiome = _context.BiomesDbSet
                        .OrderBy(b => b.NiveauRequis)
                        .FirstOrDefault();
                    
                    if (premierBiome != null)
                    {
                        lieuActuel = _context.LieuxDbSet.FirstOrDefault(l => l.BiomeId == premierBiome.Id);
                    }
                }

                // Recréer le héros
                var hero = new Hero(
                    nom: (string)data.Nom,
                    description: (string)data.Description,
                    pointsDeVie: (int)data.PointsDeVie,
                    force: (int)data.Force,
                    energie: (int)data.Energie,
                    vitesse: (int)data.Vitesse,
                    niveau: (int)data.Niveau,
                    experience: (int)data.Experience,
                    gold: (int)data.Gold,
                    lieuActuel: lieuActuel
                );

                // Charger PvMax si disponible, sinon utiliser les PV actuels ou 100
                if (data.PvMax != null)
                {
                    hero.PvMax = (int)data.PvMax;
                }
                else
                {
                    // Pour les anciennes sauvegardes, définir PvMax comme les PV actuels ou 100 minimum
                    hero.PvMax = Math.Max(100, hero.PointsDeVie);
                }

                // Charger les points de compétence si disponibles
                if (data.PointsCompetence != null)
                {
                    hero.PointsCompetence = (int)data.PointsCompetence;
                }

                // Recharger les armes
                if (data.InventaireArme != null)
                {
                    foreach (var armeData in data.InventaireArme)
                    {
                        var arme = new Arme
                        {
                            Nom = (string)armeData.Nom,
                            Valeur = (int)armeData.Valeur,
                            IntelligenceRequise = (int)armeData.IntelligenceRequise,
                            Degats = (int)armeData.Degats,
                            Type = (string)armeData.Type,
                            Effet = (EffetType)(int)armeData.Effet,
                            EffetValeur = (int)armeData.EffetValeur,
                            Rarete = (ERarete)(int)armeData.Rarete
                        };
                        hero.InventaireArme.Add(arme);
                    }
                }

                // Recharger l'arme équipée
                if (data.ArmeEquipee != null)
                {
                    var armeEquipeeData = data.ArmeEquipee;
                    hero.ArmeEquipee = new Arme
                    {
                        Nom = (string)armeEquipeeData.Nom,
                        Valeur = (int)armeEquipeeData.Valeur,
                        IntelligenceRequise = (int)armeEquipeeData.IntelligenceRequise,
                        Degats = (int)armeEquipeeData.Degats,
                        Type = (string)armeEquipeeData.Type,
                        Effet = (EffetType)(int)armeEquipeeData.Effet,
                        EffetValeur = (int)armeEquipeeData.EffetValeur,
                        Rarete = (ERarete)(int)armeEquipeeData.Rarete
                    };
                }

                // Recharger les armures
                if (data.InventaireArmure != null)
                {
                    foreach (var armureData in data.InventaireArmure)
                    {
                        var armure = new Armure(
                            nom: (string)armureData.Nom,
                            valeur: (int)armureData.Valeur,
                            intelligenceRequise: (int)armureData.IntelligenceRequise,
                            defense: (int)armureData.Defense,
                            type: "",
                            effet: (EffetType)(int)armureData.Effet,
                            effetValeur: (int)armureData.EffetValeur,
                            rarete: (ERarete)(int)armureData.Rarete
                        );
                        hero.InventaireArmure.Add(armure);
                    }
                }

                // Recharger l'armure équipée
                if (data.ArmureEquipee != null)
                {
                    var armureEquipeeData = data.ArmureEquipee;
                    hero.ArmureEquipee = new Armure(
                        nom: (string)armureEquipeeData.Nom,
                        valeur: (int)armureEquipeeData.Valeur,
                        intelligenceRequise: (int)armureEquipeeData.IntelligenceRequise,
                        defense: (int)armureEquipeeData.Defense,
                        type: "",
                        effet: (EffetType)(int)armureEquipeeData.Effet,
                        effetValeur: (int)armureEquipeeData.EffetValeur,
                        rarete: (ERarete)(int)armureEquipeeData.Rarete
                    );
                }

                // Recharger les consommables
                if (data.InventaireConsommable != null)
                {
                    foreach (var consoData in data.InventaireConsommable)
                    {
                        var consommable = new Consommable(
                            nom: (string)consoData.Nom,
                            valeur: (int)consoData.Valeur,
                            intelligenceRequise: (int)consoData.IntelligenceRequise,
                            nombreUtilisation: (int)consoData.NombreUtilisation,
                            effet: (EffetType)(int)consoData.Effet,
                            effetValeur: (int)consoData.EffetValeur,
                            rarete: (ERarete)(int)consoData.Rarete
                        );
                        hero.InventaireConsommable.Add(consommable);
                    }
                }

                Console.WriteLine($"[OK] Partie de {nomHero} chargée depuis le fichier JSON !");
                return hero;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement de {nomHero}: {ex.Message}");
                return null;
            }
        }

        public List<Hero> ChargerTous()
        {
            var heros = new List<Hero>();
            
            if (!Directory.Exists(DATABASE_PATH))
            {
                return heros;
            }

            var files = Directory.GetFiles(DATABASE_PATH, "*.json");
            
            foreach (var file in files)
            {
                try
                {
                    var nomFichier = Path.GetFileNameWithoutExtension(file);
                    var hero = Charger(nomFichier);
                    
                    if (hero != null)
                    {
                        heros.Add(hero);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors du chargement de {file}: {ex.Message}");
                }
            }

            return heros;
        }

        public void Supprimer(string nomHero)
        {
            var filePath = $"{DATABASE_PATH}{nomHero}.json";
            
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"[OK] Sauvegarde de {nomHero} supprimée (fichier JSON).");
            }
            else
            {
                Console.WriteLine($"Aucune sauvegarde trouvée pour {nomHero}.");
            }
        }
    }
}
