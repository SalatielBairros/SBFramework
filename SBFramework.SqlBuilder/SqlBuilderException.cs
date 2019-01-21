using System;

namespace SBFramework.SqlBuilder
{
  public class SqlBuilderException : Exception
  {
    private SqlBuilderException()
    {
      
    }

    private SqlBuilderException(string message)
      : base(message)
    {

    }

    public static SqlBuilderException NoWhereClauseDefined() 
      => new SqlBuilderException(Properties.Resources.WhereClausuleNotDefined);

    public static SqlBuilderException NoMapDefined() 
      => new SqlBuilderException(Properties.Resources.NoMapDefined);
  }
}