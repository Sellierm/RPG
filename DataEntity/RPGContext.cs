using Microsoft.EntityFrameworkCore;
using DataEntity.Objets;
using DataEntity.Personnages;

namespace DataEntity
{
    public class RPGContext : DbContext
    {
        private string _dbPath;
        public DbSet<Consommable> ConsommablesDbSet { get; set; }
        public DbSet<Hero> HerosDbSet { get; set; }

        public RPGContext()
        {
            _dbPath = "../../../../Database/RPG.db";
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