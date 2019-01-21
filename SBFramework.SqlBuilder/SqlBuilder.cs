using SBFramework.Base.Text;
using SBFramework.SqlBuilder.Interfaces;
using System.Text;
using SBFramework.SqlBuilder.Extensions;

namespace SBFramework.SqlBuilder
{
  public class QueryBuilder : IWhereableQuery, IOrdenableQuery, IInitialQuery, IQueryBuilder
  {
    private readonly StringBuilder _query = new StringBuilder();
    private readonly StringBuilder _whereClausule = new StringBuilder();
    private bool _whereAll, _isMapped;
    private IEntityMap _map = null;

    public string TableName { get; private set; }
    public string ParameterPrefix { get; set; } = "@";

    public string GetQuery()
    {
      string sql = _query.ToString();
      return (string.IsNullOrWhiteSpace(sql) ? _whereClausule.ToString() : sql).ToUpper().Trim();
    }

    public IQuery OrderBy(string column)
    {
      _query
        .Append(" ORDER BY ")
        .Append(column);

      return this;
    }

    public IQuery Update(params string[] columnsToUpdate)
    {
      ValidateWhereMethod();

      _query
        .Append($" UPDATE {TableName} SET ")
        .Append(columnsToUpdate.ToSetClausule(ParameterPrefix))
        .Append(_whereClausule);

      return this;
    }

    public IQuery Delete()
    {
      ValidateWhereMethod();

      _query
        .Append(" DELETE FROM ")
        .Append(TableName)
        .Append(_whereClausule);

      return this;
    }

    public IOrdenableQuery Select()
    {
      ValidateWhereMethod();

      _query
        .Append(" SELECT ")
        .Append(
          _isMapped ? _map.GetColumns().GetDbColumns().JoinByComma() : "*")
        .Append(" FROM ")
        .Append(TableName)
        .Append(_whereClausule);

      return this;
    }

    public IOrdenableQuery Select(params string[] columnsToSelect)
    {
      ValidateWhereMethod();

      _query
        .Append(" SELECT ")
        .Append(columnsToSelect.JoinByComma())
        .Append(" FROM ")
        .Append(TableName)
        .Append(_whereClausule);

      return this;
    }

    public IQuery Insert(params string[] columns)
    {
      _query
        .Append("INSERT INTO")
        .Append(TableName)
        .Append("(")
        .Append(columns.JoinByComma())
        .Append(") VALUES")
        .Append("(")
        .Append(columns.AsParameters())
        .Append(")");

      return this;
    }

    public IQuery Insert(ISelectQuery selectQuery, params string[] columns)
    {
      _query
        .Append("INSERT INTO")
        .Append(TableName)
        .Append(columns.JoinByComma())
        .Append("(")
        .Append(selectQuery.GetQuery())
        .Append(")");

      return this;
    }

    public IWhereableQuery All()
    {
      _whereAll = true;
      return this;
    }

    public IWhereableQuery Where(string clausule)
    {
      ValidateWhereMethod();

      if (!clausule.Trim().StartsWith("WHERE"))
        _whereClausule
          .Append(" WHERE ");

      _whereClausule
        .Append(clausule);

      return this;
    }

    public IInitialQuery On(string tableName)
    {
      TableName = tableName;
      return this;
    }

    public IInitialQuery On(IEntityMap map)
    {
      if (map == null) throw SqlBuilderException.NoWhereClauseDefined();

      _isMapped = true;
      TableName = map.TableName;
      _map = map;

      return this;
    }

    private void ValidateWhereMethod()
    {
      if (!_whereAll && string.IsNullOrWhiteSpace(_whereClausule.ToString()))
        throw SqlBuilderException.NoWhereClauseDefined();
    }
  }
}
