using BookRentals.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace BookRentals.Bookings.Infrastructure
{
    public class BookingsDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "bookings";
        private readonly ILoggerFactory loggerFactory;

        public BookingsDbContext(DbContextOptions<BookingsDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("name=ConnectionStrings:Bookings", providerOptions => { providerOptions.EnableRetryOnFailure(); })
                .UseLoggerFactory(loggerFactory);

            if (environment == "Development")
            {
                optionsBuilder.EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA); 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingsDbContext).Assembly);
            modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.Snapshot);
        }
    }
}
