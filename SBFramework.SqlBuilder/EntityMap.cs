using SBFramework.SqlBuilder.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SBFramework.SqlBuilder
{
  public abstract class EntityMap : IEntityMap
  {
    protected IList<IEntityColumn> Columns { get; set; }

    protected EntityMap(string tableName)
    {
      TableName = tableName;
      Columns = new List<IEntityColumn>();
    }

    public string TableName { get; protected set; }

    public IList<IEntityColumn> GetColumns() => Columns.ToList();

    public IList<IEntityColumn> GetKeys() => Columns.Where(x => x.IsKey).ToList();

    public void Map(IEntityColumn column) => Columns?.Add(column);

    public abstract void Configuration();
  }
}
