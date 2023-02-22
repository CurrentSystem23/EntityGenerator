using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.Initializer;
using EntityGenerator.Profile;
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
      ServiceProvider = CreateServiceProvider(dataAccessMock);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
      ServiceProvider?.Dispose();
    }

    protected ServiceProvider CreateServiceProvider(Mock<IDataAccessObject> dataAccessMock)
    {
      IServiceCollection serviceCollection = new ServiceCollection();
      serviceCollection.AddSingleton(new EntityGeneratorInitializer(serviceCollection));
      serviceCollection.AddTransient<MicrosoftSqlServerDao>();
      serviceCollection.AddTransient<ProfileProvider>();

      MockDataAccess(serviceCollection, dataAccessMock);

      return serviceCollection.BuildServiceProvider();
    }

    protected abstract void MockDataAccess(IServiceCollection serviceCollection, Mock<IDataAccessObject> dataAccessMock);

  }
}
