using BookRentals.Core.Infrastructure;

namespace BookRentals.Engine.Infrastructure.SqlFiles
{
    public class EngineSqlFile : SqlFile
    {
        public static SqlFile sp_GetCodeItemsSample = new EngineSqlFile(201, nameof(sp_GetCodeItemsSample), string.Format(EngineDataSeeder.StoredProceduresPathFormat, nameof(sp_GetCodeItemsSample)), SqlFileType.StoredProcedure, EngineDbContext.DEFAULT_SCHEMA);

        public EngineSqlFile(int id, string fileName, string filePath, SqlFileType sqlFileType, string schema) : base(id, fileName, filePath, sqlFileType, schema)
        {
        }
    }
}
