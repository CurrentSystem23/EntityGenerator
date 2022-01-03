using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.Initializer;
using EntityGenerator.Profile;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EntityGeneratorCLI
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("EntityGenerator V4");
      ServiceProvider serviceProvider = CreateServiceProdvider();
      ProfileProvider profileProvider = serviceProvider.GetRequiredService<ProfileProvider>();

      profileProvider.SaveProfileToFileJson(args.FirstOrDefault());
      profileProvider.SaveProfileToFileXml(args.FirstOrDefault());
    }
    protected static ServiceProvider CreateServiceProdvider()
    {
      IServiceCollection serviceCollection = new ServiceCollection();
      serviceCollection.AddSingleton(new EntityGeneratorInitializer(serviceCollection));
      serviceCollection.AddTransient<IDataAccessObject, DataAccessObject>();
      serviceCollection.AddTransient<MicrosoftSqlServerDao>();

      return serviceCollection.BuildServiceProvider();
    }
  }
}
