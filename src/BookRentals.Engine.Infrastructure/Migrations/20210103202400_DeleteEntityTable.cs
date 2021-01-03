using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookRentals.Engine.Infrastructure.Migrations
{
    public partial class DeleteEntityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deletes",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deletes", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deletes",
                schema: "engine");
        }
    }
}
