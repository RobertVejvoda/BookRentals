namespace BookRentals.Core.Infrastructure
{
    public class SqlFileType : Enumeration
    {
        public static SqlFileType Function = new SqlFileType(1, "FUNCTION");
        public static SqlFileType StoredProcedure = new SqlFileType(2, "PROCEDURE");
        public static SqlFileType View = new SqlFileType(3, "VIEW");
        public static SqlFileType Trigger = new SqlFileType(4, "TRIGGER", false);

        public bool SupportsExtendedProperties { get; }

        public SqlFileType(int id, string name, bool supportsExtendedProperties = true) : base(id, name)
        {
            SupportsExtendedProperties = supportsExtendedProperties;
        }
    }
}
