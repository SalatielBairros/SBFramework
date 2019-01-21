using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SBFramework.SqlBuilder.Interfaces
{
  public interface IEntityMap
  {
    string TableName { get; }

    IList<IEntityColumn> GetColumns();

    IList<IEntityColumn> GetKeys();

    void Map(IEntityColumn column);

    void Configuration();
  }

  public interface IEntityColumn
  {
    string EntityName { get; }
    string BdColumnName { get; }
    bool IsKey { get; }
  }

  public interface IEntityColumnBuilder<TEntity>
    where TEntity : IEntity
  {

    /// <summary>
    /// 
    /// Reference:
    /// https://stackoverflow.com/questions/2820660/get-name-of-property-as-a-string
    /// </summary>
    /// <param name="propertyLambda"></param>
    /// <returns></returns>
    IEntityColumnBuilder<TEntity> From(Expression<Func<TEntity>> propertyLambda);
    IEntityColumnBuilder<TEntity> To(string bdColumnName);
    IEntityColumnBuilder<TEntity> AsKey();
    IEntityColumn Column();
  }

  public interface IEntity
  {

  }
}
