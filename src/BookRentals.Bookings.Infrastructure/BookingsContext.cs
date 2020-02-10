using BookRentals.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRentals.Bookings.Infrastructure
{
    public class BookingsContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "bookings";

        public BookingsContext(DbContextOptions<BookingsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new BookingEntityConfiguration());
        }
    }

    public class BookingContextDesignFactory : IDesignTimeDbContextFactory<BookingsContext>
    {
        public BookingsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookingsContext>()
                .UseSqlServer("Data Source=.\\SQLExpress;Initial Catalog=BookRentals;MultipleActiveResultSets=True;Integrated Security=true;");

            return new BookingsContext(optionsBuilder.Options);
        }
    }
}
