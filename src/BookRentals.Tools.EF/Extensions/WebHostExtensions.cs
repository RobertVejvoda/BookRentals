using BookRentals.Engine.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookRentals.Tools.EF.Extensions
{
    public static class WebHostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<EngineDbContext>();
                context.Database.Migrate();
                // todo: add other contexts to migrate
            }
            return host;
        }

        public static IHost SeedDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<EngineDbContext>();
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                new EngineDataSeeder(context, loggerFactory).Run();
                // todo: add other contexts to seed
            }
            return host;
        }
    }
}
