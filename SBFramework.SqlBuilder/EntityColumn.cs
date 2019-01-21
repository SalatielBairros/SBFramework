using System;
using System.Linq.Expressions;
using SBFramework.SqlBuilder.Interfaces;

namespace SBFramework.SqlBuilder
{
  public class EntityColumn<TEntity> : IEntityColumn
    where TEntity : IEntity
  {
    private EntityColumn(string entityName, string bdColumnName, bool isKey)
    {
      EntityName = entityName;
      BdColumnName = bdColumnName;
      IsKey = isKey;
    }

    public string EntityName { get; private set; }
    public string BdColumnName { get; private set; }
    public bool IsKey { get; private set; }

    public class Builder : IEntityColumnBuilder<TEntity>
    {
      private Builder()
      {
        
      }

      private string _entityName = string.Empty, _bdColumnName = string.Empty;
      private bool _isKey = false;

      public static Builder Create() => new Builder();

      public IEntityColumnBuilder<TEntity> From(Expression<Func<TEntity>> propertyLambda)
      {
        if (!(propertyLambda.Body is MemberExpression me))
        {
          throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
        }

        _entityName = me.Member.Name;
        return this;
      }

      public IEntityColumnBuilder<TEntity> To(string bdColumnName)
      {
        _bdColumnName = bdColumnName;
        return this;
      }

      public IEntityColumnBuilder<TEntity> AsKey()
      {
        _isKey = true;
        return this;
      }

      public IEntityColumn Column()
      {
        return new EntityColumn<TEntity>(_entityName, _bdColumnName, _isKey);
      }
    }
  }
}