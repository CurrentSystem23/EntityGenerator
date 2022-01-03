using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.Profile;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EntityGenerator.ModelObjects
{
  public class ModelProvider
  {
    public ModelProvider(IServiceProvider serviceProvider, ProfileProvider profileProvider)
    {
      DataAccessObject = serviceProvider.GetRequiredService<IDataAccessObject>();
      ProfileProvider = profileProvider;
    }

    public IDataAccessObject DataAccessObject { get; }
    public ProfileProvider ProfileProvider { get; }
  }
}
