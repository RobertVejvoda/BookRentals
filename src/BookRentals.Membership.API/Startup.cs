using BookRentals.Membership.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Reflection;

namespace BookRentals.Membership.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:44319";
                options.RequireHttpsMetadata = false;
                options.Audience = "bookrentals.membership.api";
            });

            services.AddEntityFrameworkSqlServer()
                  .AddDbContext<MembershipDbContext>(options =>
                  {
                      options.UseSqlServer(Configuration["ConnectionStrings:Membership"],
                          sqlServerOptionsAction: sqlOptions =>
                          {
                              sqlOptions.MigrationsAssembly(typeof(MembershipDbContext).GetTypeInfo().Assembly.GetName().Name);
                              sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                          });
                  },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                  );

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Book Rentals API";
                    document.Info.Description = "Rent books, not buy!";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Bibliotheca co.",
                        Email = string.Empty,
                        Url = "https://bookrentals.cz/"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // register swagger
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
