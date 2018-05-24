using System;
using System.Collections.Generic;
using System.Linq;

namespace SBFramework.Base.SQL
{
    public class SqlTable
    {
        private SqlTable(string tableName)
        {
            TableName = tableName;
            Columns = new List<SqlColumn>();
            DataRows = new List<List<string>>();
        }

        public static SqlTable Create(string tableName)
        {
            return new SqlTable(tableName);
        }

        public string TableName { get; set; }
        public List<SqlColumn> Columns { get; set; }
        public List<List<string>> DataRows { get; set; }

        public SqlTable SetColumns(string[] columns)
        {
            Columns.AddRange(columns.Select(SqlColumn.Create));
            return this;
        }

        public void SetDataRows(IEnumerable<string[]> data)
        {
            DataRows.AddRange(data.Select(x => x.ToList()).ToList());
        }

        public override string ToString()
        {
            if (!Columns.Any()) throw new InvalidOperationException();

            var sqlQuery =
                $"CREATE TABLE {TableName} ({string.Join(",", Columns.Select(x => x.ToString()).ToArray())})";

            if (!DataRows.Any()) return sqlQuery;

            sqlQuery += Environment.NewLine;
            sqlQuery += $"INSERT INTO {TableName} ({string.Join(",", Columns.Select(x => x.ColumnName))}) " +
                        $"VALUES {string.Join(",", DataRows.Where(x => x.Count == Columns.Count).Select(x => $"{Environment.NewLine} ('{string.Join("','", x.Select(y => y.Replace("'", string.Empty)))}')"))}";
            return sqlQuery;
        }
    }
}