namespace SBFramework.SqlBuilder.Interfaces
{
  public interface IQueryBuilder
  {
    IInitialQuery On(string tableName);

    IInitialQuery On(IEntityMap map);
  }

  public interface IQuery
  {
    string GetQuery();

    string TableName { get; }
  }

  public interface IInitialQuery : IInsertQuery, IWhereQuery
  {

  }

  public interface IInsertQuery : IQuery
  {
    IQuery Insert(params string[] columns);

    IQuery Insert(ISelectQuery selectQuery, params string[] columns);
  }

  public interface IWhereQuery : IQuery
  {
    IWhereableQuery All();

    IWhereableQuery Where(string clausule);
  }

  public interface IDeleteQuery : IQuery
  {
    IQuery Delete();
  }

  public interface IUpdateQuery : IQuery
  {
    IQuery Update(params string[] columnsToUpdate);
  }

  public interface ISelectQuery : IQuery
  {
    IOrdenableQuery Select();

    IOrdenableQuery Select(params string[] columnsToSelect);
  }

  public interface IWhereableQuery : IDeleteQuery, IUpdateQuery, ISelectQuery
  {

  }

  public interface IOrdenableQuery : IQuery
  {
    IQuery OrderBy(string column);
  }
}
