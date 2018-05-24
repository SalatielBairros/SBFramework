namespace SBFramework.Base.SQL
{
    public class SqlColumn
    {
        public const string DefaultDataType = "VARCHAR(500)";

        private SqlColumn(string columnName)
        {
            ColumnName = columnName;
            DataType = DefaultDataType;
            Null = false;
            Pk = false;
        }

        public SqlColumn(string columnName, string dataType) : this(columnName)
        {
            DataType = dataType;
        }

        public static SqlColumn Create(string columnName)
        {
            return new SqlColumn(columnName);
        }

        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public bool Null { get; set; }
        public bool Pk { get; set; }

        private string NullDef => $"{(!Null ? "NOT" : string.Empty)} NULL";
        private string PkDef => $"{(Pk ? "PRIMARY KEY" : string.Empty)}";

        public override string ToString()
        {
            return $" {ColumnName} {DataType} {NullDef} {PkDef}";
        }
    }
}