using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SBFramework.SqlBuilder.Interfaces;

namespace SBFramework.SqlBuilder.Tests
{
  [TestClass]
  public class SqlBuilderTests
  {
    private readonly IQueryBuilder queryBuilder = new QueryBuilder();

    [TestMethod]
    public void Should_GenerateQuerySelectAllWithAllCollumns()
    {
      string query =
        queryBuilder
          .On("TEST_TABLE")
          .All()
          .Select()
          .GetQuery();

      Assert.AreEqual(
        "SELECT * FROM TEST_TABLE",
        query);
    }
  }
}
