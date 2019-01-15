
namespace SBFramework.SqlBuilder.Interfaces
{
  public interface IQueryBuilder
  {
    IInitialQuery On(string tableName);
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

    IQuery Insert(ISelectQuery selectQuery);
  }

  public interface IWhereQuery : IQuery
  {
    IWhereableQuery All();

    IWhereableQuery Where();
  }

  public interface IDeleteQuery : IQuery
  {
    IQuery Delete();
  }

  public interface IUpdateQuery : IQuery
  {
    IQuery Update();
  }

  public interface ISelectQuery : IQuery
  {
    IOrdenableQuery Select();
  }

  public interface IWhereableQuery : IDeleteQuery, IUpdateQuery, ISelectQuery
  {

  }

  public interface IOrdenableQuery : IQuery
  {
    IQuery OrderBy();
  }
}
