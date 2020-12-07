using BookRentals.Core.Infrastructure;
using BookRentals.Membership.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Membership.Infrastructure
{
    public class MembershipContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "membership";
        private readonly ILoggerFactory loggerFactory;

        public MembershipContext(DbContextOptions<MembershipContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlServer("name=ConnectionStrings:Membership", providerOptions => { providerOptions.EnableRetryOnFailure(); })
                .UseLoggerFactory(loggerFactory);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MemberEntityConfiguraration).Assembly);
        }
    }
}
