using BookRentals.Bookings.Infrastructure.Configuration;
using BookRentals.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace BookRentals.Bookings.Infrastructure
{
    public class BookingsContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "bookings";
        private readonly ILoggerFactory loggerFactory;

        public BookingsContext(DbContextOptions<BookingsContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlServer("name=ConnectionStrings:Bookings", providerOptions => { providerOptions.EnableRetryOnFailure(); })
                .UseLoggerFactory(loggerFactory);
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchiveEntityConfiguration).Assembly);
        }
    }

    public class BookingContextDesignFactory : IDesignTimeDbContextFactory<BookingsContext>
    {
        private readonly ILoggerFactory loggerFactory;

        public BookingContextDesignFactory(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public BookingsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookingsContext>()
                .EnableSensitiveDataLogging()
                .UseSqlServer("name=ConnectionStrings:Bookings", providerOptions => { providerOptions.EnableRetryOnFailure(); })
                .UseLoggerFactory(loggerFactory);

            return new BookingsContext(optionsBuilder.Options, loggerFactory);
        }
    }
}
