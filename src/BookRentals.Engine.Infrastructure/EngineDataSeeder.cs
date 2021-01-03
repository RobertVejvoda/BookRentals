using BookRentals.Core;
using BookRentals.Core.Domain;
using BookRentals.Core.Infrastructure;
using BookRentals.Engine.Infrastructure.Entities;
using BookRentals.Engine.Infrastructure.SqlFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRentals.Engine.Infrastructure
{
    public class EngineDataSeeder : DataSeederBase
    {
        public const string StoredProceduresPathFormat = "BookRentals.Engine.Infrastructure.SqlFiles.StoredProcedures.{0}.sql";
        public const string FunctionsPathFormat = "BookRentals.Engine.Infrastructure.SqlFiles.Functions.{0}.sql";
        public const string ViewsPathFormat = "BookRentals.Engine.Infrastructure.SqlFiles.Views.{0}.sql";
        public const string TriggersPathFormat = "BookRentals.Engine.Infrastructure.SqlFiles.Triggers.{0}.sql";
        private readonly EngineDbContext context;
        private readonly ILoggerFactory loggerFactory;

        public EngineDataSeeder(EngineDbContext context, ILoggerFactory loggerFactory) : base(context)
        {
            this.context = context;
            this.loggerFactory = loggerFactory;
        }

        protected override IEnumerable<SqlFile> GetSqlFilesToSeed() => Enumeration.GetAll<EngineSqlFile>();

        protected override void SeedData()
        {
            SeedCurrency();
        }

        private void SeedCurrency()
        {
            var logger = loggerFactory.CreateLogger<EngineDataSeeder>();
            using (logger.BeginScope("Currency DataSeeder"))
            {
                // add code group
                var codeGroups = Context.Set<CodeGroupEntity>().AsNoTracking().ToList();
                var codeGroup = codeGroups.FirstOrDefault(x => x.CodeGroupRef == nameof(Currency))
                        ?? new CodeGroupEntity();

                // handling insert only
                if (codeGroup.IsNew())
                {
                    codeGroup.Caption = nameof(Currency);
                    codeGroup.CodeGroupRef = nameof(Currency);
                    codeGroup.Description = "Keeps set of currencies";
                    codeGroup.ModifiedById = ModifiedByDataSeeder;
                    codeGroup.ModifiedOn = DateTime.UtcNow;

                    Context.Add(codeGroup);
                    logger.LogInformation("CodeGroupEntity {0} {1}", codeGroup.CodeGroupRef, codeGroup.IsNew() ? "inserted" : "updated");
                }

                // add code items
                var codeItems = Context.Set<CodeItemEntity>().AsNoTracking().ToList();
                foreach (var currency in Enumeration.GetAll<Currency>())
                {
                    var codeItem = codeItems.FirstOrDefault(x => x.CodeItemRef.Equals(currency.Code))
                        ?? new CodeItemEntity();

                    // handling insert only
                    if (codeItem.IsNew())
                    {
                        codeItem.CodeItemRef = currency.Code;
                        codeItem.Symbol = currency.Symbol;
                        codeItem.Caption = currency.Name;
                        codeItem.ModifiedById = ModifiedByDataSeeder;
                        codeItem.ModifiedOn = DateTime.UtcNow;

                        Context.Add(codeItem);
                        logger.LogInformation("CodeItemEntity {0} {1}", codeItem.CodeItemRef, codeItem.IsNew() ? "inserted" : "updated");
                    }
                }

                Context.SaveChanges();
            }
        }
    }
}
