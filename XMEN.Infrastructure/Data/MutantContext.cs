using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using XMEN.Core.Entities;

namespace XMEN.Infrastructure.Configurations.Data
{
    public partial class MutantContext : DbContext
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

    public class MutantContextFactory : IDesignTimeDbContextFactory<MutantContext>
    {
        public MutantContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.Production.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<MutantContext>();
            var connectionString = configuration.GetConnectionString("XMenDB");

            builder.UseNpgsql(connectionString);

            return new MutantContext(builder.Options);
        }
    }
}
