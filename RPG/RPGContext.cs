using Microsoft.EntityFrameworkCore;

namespace DataEntity
{
    public class RPGContext : DbContext
    {
        private string _dbPath;
        public DbSet<Consommable> ConsommablesDbSet { get; set; }
        public DbSet<Personnage> PersonnagesDbSet { get; set; }
        // Ajoutez d'autres DbSet pour les ennemis, cartes, etc.

        public RPGContext()
        {
            _dbPath = "RPG.db";
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}