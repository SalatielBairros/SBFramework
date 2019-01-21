using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBFramework.SqlBuilder.Interfaces;

namespace SBFramework.SqlBuilder.Extensions
{
  public static class EntityColumnExtensions
  {
    public static string[] GetDbColumns(this IList<IEntityColumn> @this) 
      => @this.Select(x => x.BdColumnName).ToArray();

    public static string[] GetEntityColumns(this IList<IEntityColumn> @this) 
      => @this.Select(x => x.EntityName).ToArray();
  }
}
