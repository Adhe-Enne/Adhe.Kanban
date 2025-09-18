using Kanban.DatabaseContext.Configurations;
using Kanban.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Reflection;
using Task = Kanban.Domain.Models.Task;

namespace Kanban.DatabaseContext
{
    public class TechnicalTestDbContext : DbContext
    {
        public TechnicalTestDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<Column> Columns => Set<Column>();
        public DbSet<Board> Boards => Set<Board>();
        public DbSet<User> Users => Set<User>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new BoardConfigurations());
            modelBuilder.ApplyConfiguration(new ColumnConfigurations());
            modelBuilder.ApplyConfiguration(new TaskConfigurations());
        }        
        // Opcional: override SaveChanges para actualizar UpdatedAt automáticamente
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseModel)
                .Where(e => e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseModel)
                .Where(e => e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
    

