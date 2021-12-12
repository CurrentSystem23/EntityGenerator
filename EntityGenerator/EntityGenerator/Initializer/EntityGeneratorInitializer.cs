using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.Profile;
using Microsoft.Extensions.DependencyInjection;

namespace EntityGenerator.Initializer
{
  public class EntityGeneratorInitializer
  {
    public EntityGeneratorInitializer(IServiceCollection services)
    {
      services.AddSingleton<ProfileProvider>();
      services.AddTransient<MicrosoftSqlServerDataAccessObject>();
    }
  }
}
