using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookRentals.Core.Infrastructure.Extensions
{
    public static class EnableTemporalDataOnTable
    {
        public static void AddAsTemporalTable(this MigrationBuilder migrationBuilder, IEntityType entityType, string temporalScheme, string temporalTableName)
        {
            var tableName = entityType.GetTableName();
            var schemaName = entityType.GetSchema() ?? "dbo";
            migrationBuilder.Sql($@"
                    IF NOT EXISTS (SELECT * FROM sys.[tables] t INNER JOIN sys.schemas s ON s.schema_id = t.schema_id WHERE t.name = '{tableName}' AND temporal_type = 2 and s.name = '{schemaName}')
                    BEGIN
                        ALTER TABLE {schemaName}.{tableName}   
                        ADD  SysStartTime datetime2 (2) GENERATED ALWAYS AS ROW START HIDDEN    
                                constraint DF_{schemaName}_{tableName}_SysStartTime DEFAULT DATEADD(second, -1, SYSUTCDATETIME())  
                            , SysEndTime datetime2 (2) GENERATED ALWAYS AS ROW END HIDDEN     
                                constraint DF_{schemaName}_{tableName}_SysEndTime DEFAULT '9999.12.31 23:59:59.99'  
                            , PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime);   
 
                        ALTER TABLE {schemaName}.{tableName}    
                        SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = {temporalScheme}.{temporalTableName})); 
                    END
                ");

        }

        public static void AddAsTemporalTable(this MigrationBuilder migrationBuilder, IEntityType entityType, string temporalScheme)
        {
            var tableName = entityType.GetTableName();
            AddAsTemporalTable(migrationBuilder, entityType, temporalScheme, $"{tableName}_History");
        }

        public static void RemoveAsTemporalTable(this MigrationBuilder migrationBuilder, IEntityType entityType, string temporalScheme, string temporalTableName)
        {
            var tableName = entityType.GetTableName();
            var schemaName = entityType.GetSchema() ?? "dbo";
            string alterStatement = $@"ALTER TABLE {schemaName}.{tableName} SET (SYSTEM_VERSIONING = OFF);";
            migrationBuilder.Sql(alterStatement);
            alterStatement = $@"ALTER TABLE {schemaName}.{tableName} DROP PERIOD FOR SYSTEM_TIME";
            migrationBuilder.Sql(alterStatement);
            alterStatement = $@"ALTER TABLE {schemaName}.{tableName} DROP DF_{schemaName}_{tableName}_SysStartTime, DF_{schemaName}_{tableName}_SysEndTime";
            migrationBuilder.Sql(alterStatement);
            alterStatement = $@"ALTER TABLE {schemaName}.{tableName} DROP COLUMN SysStartTime, COLUMN SysEndTime";
            migrationBuilder.Sql(alterStatement);
            alterStatement = $@"DROP TABLE {temporalScheme}.{temporalTableName}";
            migrationBuilder.Sql(alterStatement);
        }

        public static void RemoveAsTemporalTable(this MigrationBuilder migrationBuilder, IEntityType entityType, string temporalScheme)
        {
            var tableName = entityType.GetTableName();
            RemoveAsTemporalTable(migrationBuilder, entityType, temporalScheme, $"{tableName}_History");
        }
    }
}
