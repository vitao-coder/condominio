using Condominio.BaseModel;
using Condominio.Repository.Contracts;
using Condominio.Repository.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Condominio.Repository
{
    public class MainContext: DbContext, IMainContext
    {
        protected string _connectionString { get; private set; }

        public MainContext(string connectionString) => this._connectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseMap).Assembly);
        }

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : BaseEntity => Set<TEntity>();

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public override int SaveChanges() => base.SaveChanges();
    }
}

