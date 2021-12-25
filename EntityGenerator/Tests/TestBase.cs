using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.Initializer;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Tests
{
  public abstract class TestBase
  {
    protected ServiceProvider? ServiceProvider;

    [OneTimeSetUp]
    public void SetUp()
    {
      Mock<IDataAccessObject> dataAccessMock = new Mock<IDataAccessObject>();
      ServiceProvider = CreateServiceProdvider(dataAccessMock);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
      ServiceProvider?.Dispose();
    }

    protected ServiceProvider CreateServiceProdvider(Mock<IDataAccessObject> dataAccessMock)
    {
      IServiceCollection serviceCollection = new ServiceCollection();
      serviceCollection.AddSingleton(new EntityGeneratorInitializer(serviceCollection));

      MockDataAccess(serviceCollection, dataAccessMock);

      return serviceCollection.BuildServiceProvider();
    }

    protected abstract void MockDataAccess(IServiceCollection serviceCollection, Mock<IDataAccessObject> dataAccessMock);

  }
}
