using EntityGenerator.Initializer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGeneratorTests
{
  public class UnitTestFixture : IDisposable
  {
    internal ServiceProvider _serviceProvider;
    public UnitTestFixture()
    {
      _serviceProvider = CreateServiceProdvider();
    }

    public void SomeMethod()
    {
      Console.WriteLine("SomeFixture::SomeMethod()");
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
