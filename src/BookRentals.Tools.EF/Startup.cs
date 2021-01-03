using BookRentals.Bookings.Infrastructure;
using BookRentals.Engine.Infrastructure;
using BookRentals.Mediotheca.Infrastructure;
using BookRentals.Membership.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BookRentals.Tools.EF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EngineDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Engine").EnableDetailedErrors().LogTo(Console.WriteLine));
            services.AddDbContext<MediothecaDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Mediotheca").EnableDetailedErrors());
            services.AddDbContext<BookingsDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Bookings").EnableDetailedErrors());
            services.AddDbContext<MembershipDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Membership").EnableDetailedErrors().LogTo(Console.WriteLine));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
