namespace BookRentals.Core.Infrastructure
{
    public class SqlFile : Enumeration
    {
        public string FilePath { get; }
        public SqlFileType SqlFileType { get; }
        public string Schema { get; }

        public SqlFile(int id, string fileName, string filePath, SqlFileType sqlFileType, string schema) : base(id, fileName)
        {
            FilePath = filePath;
            SqlFileType = sqlFileType;
            Schema = schema;
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
