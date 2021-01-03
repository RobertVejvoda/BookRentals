using BookRentals.Core.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace BookRentals.Core.Infrastructure
{
    public abstract class DataSeederBase : IDataSeeder
    {
        public const int ModifiedByDataSeeder = 10000;

        protected readonly DbContext Context;
        private readonly string Md5Hash = nameof(Md5Hash);
        private readonly Assembly assembly;

        public DataSeederBase(DbContext context)
        {
            Guard.ArgumentNotNull(context, nameof(context));

            this.Context = context;
            this.assembly = context.GetType().Assembly;
        }

        public void Run()
        {
            Context.Database.EnsureCreated();

            // update SqlFiles if needed
            // Sproc, func, views and triggers which are not part of migrations
            var sqlFiles = GetSqlFilesToSeed();
            SeedSqlFiles(sqlFiles);

            // seed custom values
            SeedData();
        }

        protected virtual void SeedData() { }

        protected virtual IEnumerable<SqlFile> GetSqlFilesToSeed() => Enumerable.Empty<SqlFile>();

        /// <summary>
        /// Seed SQL files. Compare MD5 hash of the file content and update DB if it's different.
        /// It's added as Extended Property on the database object.
        /// </summary>
        /// <Remarks>
        /// Please note it's not supported for SQL Triggers so these are updated always (and use them wisely).
        /// </Remarks>
        protected void SeedSqlFiles(IEnumerable<SqlFile> sqlFiles)
        {
            foreach (var sqlFile in sqlFiles)
            {
                // get content
                var sqlContent = LoadSqlFile(sqlFile);
                if (!sqlFile.SqlFileType.SupportsExtendedProperties)
                {
                    ExecuteSqlCommand(sqlContent);
                    continue;
                }

                // calculate hash
                var md5Hash = ComputeHashFromStringContent(sqlContent);

                //  check MD5 hash from extended properties for this file in database
                var dbMd5Hash = GetMd5Hash(sqlFile);

                // compare hashes
                if (string.IsNullOrWhiteSpace(dbMd5Hash))
                {
                    // no hash yet
                    ExecuteSqlCommand(sqlContent);
                    InsertMd5Hash(sqlFile, md5Hash);
                }
                else if (string.Compare(md5Hash, dbMd5Hash) != 0)
                {
                    // hash is different so likely content as well
                    ExecuteSqlCommand(sqlContent);
                    UpdateMd5Hash(sqlFile, md5Hash);
                }
            }
        }

        protected string LoadSqlFile(SqlFile sqlFile)
        {
            string content;
            using (var stream = assembly.GetManifestResourceStream(sqlFile.FilePath))
            using (var sr = new StreamReader(stream, Encoding.UTF8))
            {
                content = sr.ReadToEnd();
            }
            return content;
        }

        protected void ExecuteSqlCommand(string content)
        {
            Context.Database.ExecuteSqlRaw(content);
        }

        protected string GetMd5Hash(SqlFile sqlFile)
        {
            var dbParameter = new SqlParameter("@value", System.Data.SqlDbType.Variant)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var selectSql = @$"select @value = value from ::fn_listextendedproperty(N'{Md5Hash}', N'SCHEMA', N'{sqlFile.Schema}', N'{sqlFile.SqlFileType.Name}', N'{sqlFile.Name}', null, null)";
            var result = Context.Database.ExecuteSqlRaw(selectSql, dbParameter);
            return dbParameter.Value as string;
        }

        protected void InsertMd5Hash(SqlFile sqlFile, string hash)
        {
            var command = @$"EXEC sys.sp_addextendedproperty 
	                            @name=N'{Md5Hash}', 
	                            @value=N'{hash}' , 
	                            @level0type=N'SCHEMA',
	                            @level0name=N'{sqlFile.Schema}', 
	                            @level1type=N'{sqlFile.SqlFileType.Name}',
	                            @level1name=N'{sqlFile.Name}'";

            Context.Database.ExecuteSqlRaw(command);
        }

        protected void UpdateMd5Hash(SqlFile sqlFile, string hash)
        {
            var command = @$"EXEC sys.sp_updateextendedproperty 
	                            @name=N'{Md5Hash}', 
	                            @value=N'{hash}' , 
	                            @level0type=N'SCHEMA',
	                            @level0name=N'{sqlFile.Schema}', 
	                            @level1type=N'{sqlFile.SqlFileType.Name}',
	                            @level1name=N'{sqlFile.Name}'";

            Context.Database.ExecuteSqlRaw(command);
        }

        protected string ComputeHashFromStringContent(string content)
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            using var hashService = MD5.Create();
            var hash = hashService.ComputeHash(stream);
            var hashString = Convert.ToBase64String(hash);
            return hashString;
        }

        private class ExtendedPropertyQueryResult
        {
            public string ObjType { get; set; }
            public string ObjName { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}