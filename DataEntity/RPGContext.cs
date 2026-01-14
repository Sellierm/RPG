using Microsoft.EntityFrameworkCore;
namespace DataEntity
{
    public class RPGContext : DbContext
    {
        private string _dbPath;
        public DbSet<Consommable> ConsommablesDbSet { get; set; }
        public DbSet<Carte> CartesDbSet { get; set; }
        public DbSet<Hero> HerosDbSet { get; set; }
        public DbSet<Arme> ArmesDbSet { get; set; }
        public DbSet<Armure> ArmuresDbSet { get; set; }
        public DbSet<Ennemi> EnnemisDbSet { get; set; }
        public DbSet<Biome> BiomesDbSet { get; set; }
        public DbSet<Lieu> LieuxDbSet { get; set; }
        
        // Initialise le contexte de base de données
        // Crée automatiquement la base SQLite si elle n'existe pas
        public RPGContext()
        {
            _dbPath = "../../../../Database/RPG.db";
            Database.EnsureCreated();
        }

        // Configure la connexion à la base de données SQLite
        // Active les proxies de chargement paresseux pour Entity Framework
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}