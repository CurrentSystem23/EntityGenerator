using EntityGenerator.Initializer;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Tests
{
  public class TestBase
  {
    protected ServiceProvider? ServiceProvider;

    [OneTimeSetUp]
    public void SetUp()
    {
      ServiceProvider = CreateServiceProdvider();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
      ServiceProvider?.Dispose();
    }

    protected ServiceProvider CreateServiceProdvider()
    {
      IServiceCollection serviceCollection = new ServiceCollection();
      serviceCollection.AddSingleton(new EntityGeneratorInitializer(serviceCollection));

      return serviceCollection.BuildServiceProvider();
    }

  }
}
