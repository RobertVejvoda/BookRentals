using BookRentals.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

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
    }
}
