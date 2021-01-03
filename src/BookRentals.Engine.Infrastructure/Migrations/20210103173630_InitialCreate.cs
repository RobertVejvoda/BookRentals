using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookRentals.Engine.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "engine");

            migrationBuilder.CreateTable(
                name: "CodeGroup",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CodeGroupRef = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGroup", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CodeItem",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CodeItemRef = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Symbol = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeItem", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Reference = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ConfigurationKey = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false, defaultValueSql: "'*'"),
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Caption = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CultureCode = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ModifiedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "CodeGroupItem",
                schema: "engine",
                columns: table => new
                {
                    CodeGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigurationKey = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: false, defaultValueSql: "'*'"),
                    Ident = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGroupItem", x => new { x.CodeGroupId, x.CodeItemId, x.ConfigurationKey });
                    table.ForeignKey(
                        name: "FK_CodeGroupItem_CodeGroup_CodeGroupId",
                        column: x => x.CodeGroupId,
                        principalSchema: "engine",
                        principalTable: "CodeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CodeGroupItem_CodeItem_CodeItemId",
                        column: x => x.CodeItemId,
                        principalSchema: "engine",
                        principalTable: "CodeItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeGroupItem_CodeItemId",
                schema: "engine",
                table: "CodeGroupItem",
                column: "CodeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_Reference_ConfigurationKey",
                schema: "engine",
                table: "Configuration",
                columns: new[] { "Reference", "ConfigurationKey" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeGroupItem",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "Configuration",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "CodeGroup",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "CodeItem",
                schema: "engine");
        }
    }
}
