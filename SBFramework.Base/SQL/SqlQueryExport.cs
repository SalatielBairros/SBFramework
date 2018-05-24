using System;
using System.Linq;
using SBFramework.Base.IO;

namespace SBFramework.Base.SQL
{
    public class SqlQueryExport
    {
        public static void CsvToQuery(string fileName, string tableName, string retFile, char separator = ';', bool firstLineAsColumns = true)
        {
            if(!fileName.EndsWith(".csv")) throw new ArgumentException();
            string[] fileLines = FileActions.ReadFileLines(fileName);
            if(fileLines == null || !fileLines.Any()) throw new ArgumentException();
            var table = SqlTable.Create(tableName);

            if (firstLineAsColumns)
            {
                table.SetColumns(fileLines.First().ToUpper().Split(separator));
            }

            table.SetDataRows(fileLines.Skip(firstLineAsColumns ? 1 : 0).Select(x => x.Split(separator)));
            table.ToString().WriteToFile(retFile);
        }
    }
}
