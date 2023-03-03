using EntityGenerator.CodeGeneration.Interfaces;
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
      serviceCollection.AddTransient<IInformationExtractor, EntityGenerator.InformationExtractor.MSSqlServer.Services.InformationExtractor.InformationExtractor>();
      serviceCollection.AddTransient<IInformationExtractorWorker, InformationExtractor.MSSqlServer.Services.InformationExtractor.InformationExtractorWorker>();

      serviceCollection.AddTransient<IFileWriterService, EntityGenerator.CodeGeneration.Services.FileWriterService>();
      serviceCollection.AddTransient<ILanguageProvider, EntityGenerator.CodeGeneration.Languages.LanguageProvider>();
      serviceCollection.AddTransient<ICodeGenerator, EntityGenerator.CodeGeneration.Services.CodeGenerator>();
      serviceCollection.AddTransient<ICodeGeneratorWorker, EntityGenerator.CodeGeneration.Services.CodeGeneratorWorker>();
      serviceCollection.AddTransient<ICoreServiceWorker, CoreServiceWorker>();
    }
  }
}
