using BookRentals.Bookings.Infrastructure;
using BookRentals.Engine.Infrastructure;
using BookRentals.Mediotheca.Infrastructure;
using BookRentals.Membership.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookRentals.Tools.EF
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EngineDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Engine"));
            services.AddDbContext<MediothecaDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Mediotheca"));
            services.AddDbContext<BookingsDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Bookings"));
            services.AddDbContext<MembershipDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Membership"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
