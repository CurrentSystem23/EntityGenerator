using EntityGenerator.Initializer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace UnitTests
{
  public class UnitTestFixture : IDisposable
  {
    internal ServiceProvider _serviceProvider;
    public UnitTestFixture()
    {
      _serviceProvider = CreateServiceProdvider();
    }

    public void Dispose()
    {
      _serviceProvider.Dispose();
    }
    protected ServiceProvider CreateServiceProdvider()
    {
      IServiceCollection serviceCollection = new ServiceCollection();
      serviceCollection.AddSingleton(new EntityGeneratorInitializer(serviceCollection));

      return serviceCollection.BuildServiceProvider();
    }
  }
}