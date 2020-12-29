using BookRentals.Core.Infrastructure;
using BookRentals.Membership.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace BookRentals.Membership.Infrastructure
{
    public class MembershipDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "membership";
        private readonly ILoggerFactory loggerFactory;

        public MembershipDbContext(DbContextOptions<MembershipDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("name=ConnectionStrings:Membership", providerOptions => { providerOptions.EnableRetryOnFailure(); })
                .UseLoggerFactory(loggerFactory);

            if (environment == "Development")
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MemberEntityConfiguraration).Assembly);
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot);
        }
    }
}
