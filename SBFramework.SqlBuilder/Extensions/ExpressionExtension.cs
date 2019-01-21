using System;
using System.Linq.Expressions;

namespace SBFramework.SqlBuilder.Extensions
{
  public static class ExpressionExtension
  {
    public static string PropertyName<T>(this Expression<Func<T>> propertyLambda)
    {
      if (!(propertyLambda.Body is MemberExpression me))
      {
        throw new ArgumentException(Properties.Resources.LambdaPropertyError);
      }

      return me.Member.Name;
    }
  }
}