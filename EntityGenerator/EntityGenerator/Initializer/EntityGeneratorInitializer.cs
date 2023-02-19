using EntityGenerator.Core.Interfaces;
using EntityGenerator.Core.Services;
using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.InformationExtractor.Interfaces;
using EntityGenerator.Profile;
using Microsoft.Extensions.DependencyInjection;

namespace EntityGenerator.Initializer
{
  public class EntityGeneratorInitializer
  {
    public EntityGeneratorInitializer(IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton<IProfileProvider, ProfileProvider>();
      serviceCollection.AddTransient<MicrosoftSqlServerDao>();
      serviceCollection.AddTransient<IInformationExtractor, EntityGenerator.InformationExtractor.MSSqlServer.Services.InformationExtractor>();
      serviceCollection.AddTransient<IInformationExtractorWorker, EntityGenerator.InformationExtractor.MSSqlServer.Services.InformationExtractorWorker>();
      serviceCollection.AddTransient<ICoreServiceWorker, CoreServiceWorker>();

    }
  }
}
