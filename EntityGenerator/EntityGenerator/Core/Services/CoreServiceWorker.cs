using EntityGenerator.Core.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.Interfaces;
using EntityGenerator.Profile;
using EntityGenerator.Profile.DataTransferObject;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EntityGenerator.Core.Services
{
  public class CoreServiceWorker : ICoreServiceWorker
  {
    ProfileDto _profile;

    /// <summary>
    /// The service provider.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// The profile provider.
    /// </summary>
    private readonly IProfileProvider _profileProvider;

    /// <summary>
    /// Constructor for <see cref="CoreServiceWorker"/> class.
    /// </summary>
    /// <param name="serviceProvider"> The dependency injection service provider.</param>
    /// <param name="profileProvider"> The profile provider.</param>
    public CoreServiceWorker(IServiceProvider serviceProvider, IProfileProvider profileProvider)
    {
      _serviceProvider = serviceProvider;
      _profileProvider = profileProvider;
    }

    /// <summary>
    /// For Test only!
    /// </summary>
    /// <param name="profile"></param>
    public void ExtractData(ProfileDto profile)
    {
      _profile = profile;
      IInformationExtractorWorker informationExtractorWorker = _serviceProvider.GetRequiredService<IInformationExtractorWorker>();

      informationExtractorWorker.GetDataCount(profile);
      Database database = informationExtractorWorker.ExtractData(profile);
    }

  }
}
