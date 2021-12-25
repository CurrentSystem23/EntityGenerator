using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.DatabaseObjects.DataTransferObjects;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests
{
  [TestFixture]
  public class ModelProviderTests : TestBase
  {
    [Test]
    [Category("UnitTest")]
    public void DatabaseObjectCount()
    {
      // Dieser Test testet vorerst nur den Mock selbst!
      // arrange
      IDataAccessObject? microsoftSqlServerDao = ServiceProvider?.GetRequiredService<IDataAccessObject>();

      // act
      int? dbObjectCount = microsoftSqlServerDao?.DatabaseObjectCount();

      // assert
      Assert.NotNull(dbObjectCount);
      Assert.AreEqual(dbObjectCount, 42);
    }

    [Test]
    [Category("UnitTest")]
    public void DatabaseFunctionReturnColumns()
    {
      // Dieser Test testet vorerst nur den Mock selbst!
      // arrange
      IDataAccessObject? microsoftSqlServerDao = ServiceProvider?.GetRequiredService<IDataAccessObject>();

      // act
      List<FunctionDto>? functions = microsoftSqlServerDao?.DatabaseFunctions();
      microsoftSqlServerDao?.DatabaseFunctionReturnColumns(functions);
      FunctionDto? functionGetDomainValue = functions?.FirstOrDefault(s => s.FunctionName.Equals("GetDomainValue", StringComparison.InvariantCultureIgnoreCase));
      FunctionDto? functionTableValuedFunction = functions?.FirstOrDefault(s => s.FunctionName.Equals("TableValuedFunction", StringComparison.InvariantCultureIgnoreCase));

      // assert
      Assert.NotNull(functions);
      Assert.NotNull(functionGetDomainValue);
      Assert.NotNull(functionTableValuedFunction);
      Assert.AreEqual(functions?.Count, 3);
      Assert.NotNull(functionGetDomainValue?.ReturnColumns);
      Assert.NotNull(functionTableValuedFunction?.ReturnColumns);
      Assert.AreEqual(functionGetDomainValue?.ReturnColumns.Count, 11);
      Assert.AreEqual(functionTableValuedFunction?.ReturnColumns.Count, 2);

    }

    protected override void MockDataAccess(IServiceCollection serviceCollection, Mock<IDataAccessObject> dataAccessMock)
    {
      serviceCollection.SwapTransient<IDataAccessObject>(provider => dataAccessMock.Object);

      MockSetupBase.MockSetup(dataAccessMock);
    }
  }
}
