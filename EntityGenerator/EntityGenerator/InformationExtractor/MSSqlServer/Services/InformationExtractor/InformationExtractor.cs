using EntityGenerator.Core.Interfaces;
using EntityGenerator.InformationExtractor.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.InformationExtractor;

/// <summary>
/// Class <see cref="InformationExtractor"/> contains methods for information extraction from MS SqlServer.
/// </summary>
public partial class InformationExtractor : IInformationExtractor
{
  /// <summary>
  /// The logging provider.
  /// </summary>
  private readonly ILogger<InformationExtractor> _logger;

  /// <summary>
  /// The output provider.
  /// </summary>
  private readonly IOutputProvider _outputProvider;

  #region Constructor
  /// <summary>
  /// Constructor for <see cref="InformationExtractor"/> class.
  /// </summary>
  /// <param name="provider"> The dependency injection service provider.</param>
  /// <param name="logger"> The logging provider.</param>
  /// <param name="outputProvider"> The output provider.</param>
  public InformationExtractor(ILogger<InformationExtractor> logger = null, IOutputProvider outputProvider = null)
  {
    _logger = logger;
    _outputProvider = outputProvider;
  }
  #endregion
  
  /// <summary>
  /// Increase the output step by one.
  /// </summary>
  private void IncreasePosition()
  {
    _outputProvider?.IncreasePosition();
  }
}