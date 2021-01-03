using BookRentals.Core.Infrastructure;
using BookRentals.Core.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookRentals.Engine.Infrastructure
{
    public class EngineDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "engine";
        private readonly ILoggerFactory loggerFactory;

        public EngineDbContext(DbContextOptions<EngineDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("name=ConnectionStrings:Engine", providerOptions => { providerOptions.EnableRetryOnFailure(); })
                .UseLoggerFactory(loggerFactory);

            if (environment == "Development")
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EngineDbContext).Assembly);
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot);
        }

        public override int SaveChanges()
        {
            TrackDeletedEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackDeletedEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public void TrackDeletedEntities()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries.Where(x => x.State == EntityState.Deleted))
            {
                var entityName = entry.Entity.GetType().BaseType.Name;
                this.Set<DeletedEntity>().Add(new DeletedEntity
                {
                    DeletedById = 20000,
                    DeletedOn = DateTime.UtcNow,
                    EntityId = (entry.Entity as Entity).Id,
                    EntityName = entityName
                });
            }

        }

    }
}
