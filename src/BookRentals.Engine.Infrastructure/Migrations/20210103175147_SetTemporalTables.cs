using BookRentals.Core.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookRentals.Engine.Infrastructure.Migrations
{
    public partial class SetTemporalTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityTypes = base.TargetModel.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                migrationBuilder.AddAsTemporalTable(entityType, EngineDbContext.DEFAULT_SCHEMA);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityTypes = base.TargetModel.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                migrationBuilder.RemoveAsTemporalTable(entityType, EngineDbContext.DEFAULT_SCHEMA);
            }
        }
    }
}
