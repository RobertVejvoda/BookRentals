using BookRentals.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace BookRentals.Mediotheca.Infrastructure
{
    public class MediothecaDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "media";
        private readonly ILoggerFactory loggerFactory;

        public MediothecaDbContext(DbContextOptions<MediothecaDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("name=ConnectionStrings:Mediotheca", providerOptions => { providerOptions.EnableRetryOnFailure(); })
                .UseLoggerFactory(loggerFactory);

            if (environment == "Development")
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediothecaDbContext).Assembly);
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot);
        }
    }
}
