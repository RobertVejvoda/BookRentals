using BookRentals.Tools.EF.Extensions;
using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace BookRentals.Tools.EF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    if (o.Migrate)
                    {
                        Console.WriteLine("Running Migrations");
                        host.MigrateDatabase();
                    }

                    if (o.Seed)
                    {
                        Console.WriteLine("Running DataSeeder");
                        host.SeedDatabase();
                    }

                    Environment.Exit(0);
                });

            host.Run();
        }
    

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private class Options
        {
            [Option('m', "migrate", Required = false, HelpText = "Migrate database to latest version.")]
            public bool Migrate { get; set; }

            [Option('s', "seed", Required = false, HelpText = "Seed database with custom data.")]
            public bool Seed { get; set; }
        }
    }
}
