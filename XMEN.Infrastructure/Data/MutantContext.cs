using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using XMEN.Core.Entities;

namespace XMEN.Infrastructure.Configurations.Data
{
    class MutantContext : DbContext
    {
        public MutantContext()
        {
        }
        public MutantContext(DbContextOptions<MutantContext> options) : base(options)
        {
        }

        public virtual DbSet<VerifiedDNAHistory> VerifiedDNAHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSave()
        {
            var currentTime = DateTime.UtcNow;
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is BaseEntity))
            {
                var entidad = item.Entity as BaseEntity;
                entidad.CreatedUTC = currentTime;
                entidad.UpdatedUTC = currentTime;
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(predicate: e => e.State == EntityState.Modified && e.Entity is BaseEntity))
            {
                var entidad = item.Entity as BaseEntity;
                entidad.UpdatedUTC = currentTime;
                item.Property(nameof(entidad.CreatedUTC)).IsModified = false;
            }
        }
    }
}
