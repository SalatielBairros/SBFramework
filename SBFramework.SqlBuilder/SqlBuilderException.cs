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
    {
      return new SqlBuilderException(Properties.Resources.WhereClausuleNotDefined);
    }
  }
}