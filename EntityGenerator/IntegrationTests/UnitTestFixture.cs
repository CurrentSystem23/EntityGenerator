using EntityGenerator.Initializer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IntegrationTests
{
  public class UnitTestFixture : IDisposable
  {
    public UnitTestFixture()
    {
      ServiceProvider = CreateServiceProdvider();
    }

    internal ServiceProvider ServiceProvider { get; }

    public void Dispose()
    {
      ServiceProvider.Dispose();
    }

    protected ServiceProvider CreateServiceProdvider()
    {
      IServiceCollection serviceCollection = new ServiceCollection();
      serviceCollection.AddSingleton(new EntityGeneratorInitializer(serviceCollection));

      return serviceCollection.BuildServiceProvider();
    }
  }

}